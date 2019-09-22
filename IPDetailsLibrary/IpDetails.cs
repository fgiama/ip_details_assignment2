using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPDetailsLibrary
{
    [JsonObject]
    internal class IpDetails: IIpDetails
    {
        [JsonProperty("ip")]
        public string IP { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country_name")]
        public string Country { get; set; }
        [JsonProperty("continent_name")]
        public string Continent { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("success")]
        public bool? Success { get; set; }
        [JsonProperty("error")]
        public IpDetailsError Error { get; set; }
        public IpDetails() { }
    }

    [JsonObject]
    internal class IpDetailsError
    {
        [JsonProperty("info")]
        public string Info { get; set; }
    }

}
