using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDetailsService
{
    internal static class AppSettingsManager
    {
        public static string BaseWebApiAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["BaseWebApiAddress"];
            }
        }

        public static int Interval
        {
            get
            {
                var intervalSetting = ConfigurationManager.AppSettings["IntervalInMinutes"];
                if (intervalSetting != null)
                    return Convert.ToInt32(intervalSetting);
                return 5;
            }
        }

        public static bool IncludeHeaders
        {
            get
            {
                var includeHeadersSetting = ConfigurationManager.AppSettings["IncludeHeaders"];
                if (includeHeadersSetting != null)
                    return Convert.ToBoolean(includeHeadersSetting);
                return false;
            }
        }

        public static string CSVCustomFileNameFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVCustomFileNameFormat"];
            }
        }

        public static string CSVFileDateFormat
        {
            get
            {
                return ConfigurationManager.AppSettings["CSVCustomFileDateFormat"];
            }
        }

    }
}
