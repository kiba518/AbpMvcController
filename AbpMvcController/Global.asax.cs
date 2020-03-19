using Abp.Web;
using AbpMvcController;
using System;
using System.Web;
[assembly: PreApplicationStartMethod(typeof(PreStarter), "Start")]
namespace AbpMvcController
{
    public class WebApiApplication : Abp.Web.AbpWebApplication<StartModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
        } 
    }

    public static class PreStarter
    {
        public static void Start()
        {
            //WebApiApplication.AbpBootstrapper.PlugInSources.AddFolder(System.Web.Hosting.HostingEnvironment.MapPath("~/Bundles"));
            WebApiApplication.AbpBootstrapper.PlugInSources.AddToBuildManager();

        }
    } 
}

//public class WebApiApplication : System.Web.HttpApplication
//{
//    protected void Application_Start()
//    {
//        AreaRegistration.RegisterAllAreas();
//        GlobalConfiguration.Configure(WebApiConfig.Register);
//        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
//        RouteConfig.RegisterRoutes(RouteTable.Routes);
//        BundleConfig.RegisterBundles(BundleTable.Bundles);
//    }
//}