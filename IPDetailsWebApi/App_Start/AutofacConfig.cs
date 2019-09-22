using Autofac;
using Autofac.Integration.WebApi;
using IPDetailsWebApi.Controllers;
using IPDetailsWebApi.Models.Cache;
using IPDetailsWebApi.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace IPDetailsWebApi
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterType<IPDetailsController>();
            
            builder.RegisterInstance(new MemoryClassDataStore())
                .As<ICacheDataStore>();

            builder.RegisterInstance(new SqlDataStore())
                .As<IDatabaseDataStore>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}