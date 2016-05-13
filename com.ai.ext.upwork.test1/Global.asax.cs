using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Web.Http;

namespace com.ai.ext.upwork.test1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // AISAKU
            //Added for WebApi Routing to work
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);           
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            

            //Start SqlDependency with application init
            //SqlDependency.Start(Utility.ConnString);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            //SqlDependency.Stop(Utility.ConnString);
        }
    }    
}
