﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDetailsService
{
    internal class IPAddressModel
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
