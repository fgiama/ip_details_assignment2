using System;
using System.Collections.Generic;
using System.Text;

namespace IPDetailsLibrary
{
    public interface IIpDetails
    {
        string IP { get; set; }
        string City { get; set; }
        string Country { get; set; }
        string Continent { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }

    }
}
