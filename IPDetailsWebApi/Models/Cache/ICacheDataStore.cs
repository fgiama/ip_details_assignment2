using IPDetailsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPDetailsWebApi.Models.Cache
{
    public interface ICacheDataStore
    {
        void Add(string ip, IPDetailsModel item, int? expirationInMinutes);
        IPDetailsModel Get(string ip);
    }
}