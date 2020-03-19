using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AbpMvcController
{
    [DependsOn(typeof(Abp.Web.AbpWebModule),typeof(AbpWebMvcModule))]
    public class StartModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true; 
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public override void PostInitialize()
        {
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionFilter());
        }
        public override void Shutdown()
        {
        }
    }
}