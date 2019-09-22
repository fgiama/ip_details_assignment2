using CsvHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IPDetailsService
{
    internal class IPAddressesFileProvider
    {
        internal async void CreateCSVFile(string path)
        {
            try
            {
                //get ip addresses to request for details
                var ipAddresses = GetIPAddresses();

                //Create a new csv file
                var filePath = string.Format("{0}\\CSVs\\{1}.csv", AppDomain.CurrentDomain.BaseDirectory, GetFileName());
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer))
                {
                    //set if headers will be added
                    csv.Configuration.HasHeaderRecord = AppSettingsManager.IncludeHeaders;

                    if (AppSettingsManager.IncludeHeaders)
                    {
                        //write header and get to next line
                        csv.WriteHeader<IPAddressModel>();
                        csv.NextRecord();
                    }
                    //create a list with model that be written in the csv
                    var list = new List<IPAddressModelStringValues>();

                    //call web api to get ip details
                    foreach (var ip in ipAddresses)
                    {
                        if (!IPIsPrivate(ip))
                        {
                            var ipDetails = await GetIPDetails(ip);
                            list.Add(new IPAddressModelStringValues() { Ip = ipDetails.Ip, City = ipDetails.City, Continent = ipDetails.Continent, Country = ipDetails.Country, Latitude = ipDetails.Latitude.ToString(), Longitude = ipDetails.Longitude.ToString() });
                        }
                        else
                        {
                            //if is private ip then set 'N/A' to all values
                            list.Add(new IPAddressModelStringValues() { Ip = ip, City = "N/A", Continent = "N/A", Country = "N/A", Latitude = "N/A", Longitude = "N/A" });
                        }

                    }
                    //write list to csv
                    csv.WriteRecords(list);
                    writer.Flush();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool IPIsPrivate(string ip)
        {
            System.Net.IPAddress address = null;
            //check if the ip is valid
            if (System.Net.IPAddress.TryParse(ip, out address))
            {
                var parts = ip.Split('.');

                var part2Int = Convert.ToInt32(parts[1]);
                if (parts[0].Equals("10") ||
                    (parts[0].Equals("172") && part2Int >= 16 && part2Int <= 31) ||
                    (parts[0].Equals("192") && parts[1].Equals("168")))
                    return true;
            }
            else
            {
                throw new Exception("Invalid ip.");
            }

            return false;
        }

        private IEnumerable<string> GetIPAddresses()
        {
            //use sqlconnection and sqldatareader to read the addresses
            var list = new List<string>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["IPDetailsDB"].ConnectionString);
            try
            {
                SqlCommand sm = new SqlCommand();
                sm.CommandText = "select * from IPAddress";
                sm.Connection = con;

                con.Open();
                using (SqlDataReader reader = sm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }

                con.Close();
                sm.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public async Task<IPAddressModel> GetIPDetails(string ip)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //use base address from configuration
                    string baseAddress = AppSettingsManager.BaseWebApiAddress;
                    var uri = string.Format("{0}api/ipdetails/{1}", baseAddress, ip);
                    HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return ConvertResponseBody(responseBody);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private IPAddressModel ConvertResponseBody(string response)
        {
            JObject json = JObject.Parse(response);

            //check for errors
            string errorMsg = string.Empty;
            if (ResponseContainsError(json, out errorMsg))
                throw new Exception(errorMsg);

            JToken ipDetailsToken = null;
            json.TryGetValue("ip_details", out ipDetailsToken);
            //load ip details
            var ipDetails = new IPAddressModel();
            ipDetails.Ip = ipDetailsToken.Value<string>("Ip");
            ipDetails.City = ipDetailsToken.Value<string>("City");
            ipDetails.Country = ipDetailsToken.Value<string>("Country");
            ipDetails.Continent = ipDetailsToken.Value<string>("Continent");
            ipDetails.Latitude = ipDetailsToken.Value<double>("Latitude");
            ipDetails.Longitude = ipDetailsToken.Value<double>("Longitude");

            return ipDetails;

        }

        private bool ResponseContainsError(JObject json, out string errorMsg)
        {
            var success = json.Value<bool>("success");
            if (!success)
            {
                JToken error = null;
                json.TryGetValue("error", out error);
                errorMsg = error.Value<string>();
                return true;
            }
            errorMsg = string.Empty;
            return false;
        }

        private string GetFileName()
        {
            //get format from configuration
            var dateFormatStr = "{0:" + AppSettingsManager.CSVFileDateFormat + "}";
            var dateStr = string.Format(dateFormatStr, DateTime.Now);
            var fileName = string.Format("{0}", AppSettingsManager.CSVCustomFileNameFormat.Replace("{date}", dateStr));

            return fileName;
        }
    }


}
