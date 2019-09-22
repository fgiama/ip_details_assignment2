using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace IPDetailsLibrary
{
    internal class IpInfoProvider : IIpInfoProvider
    {
        private readonly string BaseUri = "http://api.ipstack.com/{0}?access_key={1}";
        private static readonly HttpClient _client;

        static IpInfoProvider()
        {
            _client = new HttpClient();
        }
        public IIpDetails GetDetails(string ip)
        {
            try
            {
                var task = GetIpDetails(ip);
                var awaiter = task.GetAwaiter();
                return awaiter.GetResult();
            }
            catch (Exception ex)
            {
                throw new IpServiceNotAccessibleException(ex.Message);
            }
            
        }

        public async Task<IIpDetails> GetIpDetails(string ip)
        {
            var apiKey = ConfigurationManager.AppSettings["apiKey"];
            var uri = string.Format(BaseUri, ip, apiKey);
            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return ConvertResponseBody(responseBody);
        }

        private IIpDetails ConvertResponseBody(string response)
        {
            IpDetails ipDetails = null;
            try
            {
                ipDetails = JsonConvert.DeserializeObject<IpDetails>(response);
            }
            catch
            {
                throw new Exception("IP not found.");
            }

            if(ipDetails.Success.HasValue && !ipDetails.Success.Value)
                throw new Exception(ipDetails.Error.Info);
          
            return ipDetails;
        }

    }
}
