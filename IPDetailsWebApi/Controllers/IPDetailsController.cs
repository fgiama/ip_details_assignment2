using IPDetailsLibrary;
using IPDetailsWebApi.Models.Cache;
using IPDetailsWebApi.Models.DB;
using System;
using IPDetailsWebApi.Models;
using System.Net;
using System.Web.Http;

namespace IPDetailsWebApi.Controllers
{
    public class IPDetailsController : ApiController
    {
        private ICacheDataStore _cacheStore;
        private IDatabaseDataStore _dbStore;

        public IPDetailsController(ICacheDataStore cacheStore, IDatabaseDataStore dbStore)
        {
            _cacheStore = cacheStore;
            _dbStore = dbStore;
        }

        
        public IHttpActionResult Get()
        {
            return Ok(_dbStore.GetAllIpDetails());
        }

        [Route("api/ipdetails/{ip}")]
        public IHttpActionResult Get(string ip)
        {
            //check if the ip is valid
            IPAddress ipAddress = null;
            if (!IPAddress.TryParse(ip, out ipAddress))
                return BadRequest("Invalid IP.");

            ip = ipAddress.ToString();

            if (IPIsPrivate(ip))
                return BadRequest("IP is private.");

            IPDetailsModel ipDetails;
            
            try
            {
                //get from cache 
                ipDetails = _cacheStore.Get(ip);
                if (ipDetails == null)
                {
                    //get from db
                    ipDetails = _dbStore.GetIpDetails(ip);
                    if (ipDetails == null)
                    {
                        //get from library
                        var factory = new IpInfoProviderFactory();
                        IIpInfoProvider provider = factory.Create();
                        ipDetails = CopyToModel(provider.GetDetails(ip), ip);

                        //if ip found in library => store into cache and db
                        _dbStore.AddIpDetails(ip, ipDetails);
                        _cacheStore.Add(ip, ipDetails, AppSettingsManager.CacheExpirationTime);
                    }
                    else
                    {
                        //if ip found in db => store into cache
                        _cacheStore.Add(ip, ipDetails, AppSettingsManager.CacheExpirationTime);
                    }
                }

                return Ok(new { success = true,  ip_details = ipDetails });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, error = ex.Message });
            }

        }

        private IPDetailsModel CopyToModel(IIpDetails ipDetails, string ip)
        {
            var model = new IPDetailsModel();
            model.Ip = ip;
            model.City = ipDetails.City;
            model.Continent = ipDetails.Continent;
            model.Country = ipDetails.Country;
            model.Latitude = ipDetails.Latitude;
            model.Longitude = ipDetails.Longitude;

            return model;
        }

        private bool IPIsPrivate(string ip)
        {
            var parts = ip.Split('.');

            var part2Int = Convert.ToInt32(parts[1]);
            if (parts[0].Equals("10") ||
                (parts[0].Equals("172") && part2Int >= 16 && part2Int <= 31) ||
                (parts[0].Equals("192") && parts[1].Equals("168")))
                return true;

            return false;
        }
    }
}
