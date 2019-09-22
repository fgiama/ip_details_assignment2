
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IPDetailsWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
