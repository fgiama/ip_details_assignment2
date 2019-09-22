using IPDetailsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDetailsWebApi.Models.DB
{
    public interface IDatabaseDataStore
    {
        void AddIpDetails(string ip, IPDetailsModel item);
        IPDetailsModel GetIpDetails(string ip);
        IEnumerable<IPDetailsModel> GetAllIpDetails();
    }
}
