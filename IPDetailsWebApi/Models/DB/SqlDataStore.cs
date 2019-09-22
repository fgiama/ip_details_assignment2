using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IPDetailsLibrary;

namespace IPDetailsWebApi.Models.DB
{
    public class SqlDataStore : IDatabaseDataStore
    {
        private readonly IPDetailsDBEntities _context;
        public SqlDataStore()
        {
            _context = new IPDetailsDBEntities();
        }

        public void AddIpDetails(string ip, IPDetailsModel item)
        {
            var dbItem = new IPDetail();
            dbItem.Ip = ip;
            dbItem.City = item.City;
            dbItem.Country = item.Country;
            dbItem.Continent = item.Continent;
            dbItem.Latitude = item.Latitude.GetValueOrDefault().ToString();
            dbItem.Longitude = item.Longitude.GetValueOrDefault().ToString();

            _context.IPDetails.Add(dbItem);

            _context.SaveChanges();
        }

        public IPDetailsModel GetIpDetails(string ip)
        {
            var dbItem = _context.IPDetails.FirstOrDefault(n => n.Ip == ip);
            IPDetailsModel ipDetails = null;

            if (dbItem != null)
            {
                ipDetails = new IPDetailsModel();
                ipDetails.Ip = ip;
                ipDetails.City = dbItem.City;
                ipDetails.Country = dbItem.Country;
                ipDetails.Continent = dbItem.Continent;
                ipDetails.Latitude = Convert.ToDouble(dbItem.Latitude == null? "0": dbItem.Latitude);
                ipDetails.Longitude = Convert.ToDouble(dbItem.Longitude == null ? "0" : dbItem.Longitude);
            }

            return ipDetails;
        }

        public IEnumerable<IPDetailsModel> GetAllIpDetails()
        {
            return _context.IPDetails.Select(n => new IPDetailsModel() { Ip = n.Ip, City = n.City, Continent = n.Continent, Country = n.Country });
        }
    }
}