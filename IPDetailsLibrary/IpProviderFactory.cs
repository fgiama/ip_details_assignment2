using System;
using System.Collections.Generic;
using System.Text;

namespace IPDetailsLibrary
{
    public class IpInfoProviderFactory
    {
        public IIpInfoProvider Create() => new IpInfoProvider();
    }
}
