using IPDetailsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace IPDetailsWebApi.Models.Cache
{
    public class MemoryClassDataStore : ICacheDataStore
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        public void Add(string ip, IPDetailsModel item, int? expirationInMinutes = 1)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(expirationInMinutes.Value);
            Cache.Add(ip, item, policy);
        }

        public IPDetailsModel Get(string ip)
        {
            return Cache[ip] as IPDetailsModel;
        }
    }
}