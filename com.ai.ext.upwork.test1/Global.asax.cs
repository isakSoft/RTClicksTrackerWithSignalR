using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using com.ai.ext.upwork.test1.Models;
using System.Data.SqlClient;
using Owin;

namespace com.ai.ext.upwork.test1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);           
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Start SqlDependency with application init
            SqlDependency.Start(Utility.ConnString);
        }

        protected void Application_End()
        {
            //Stop SQL dependency
            SqlDependency.Stop(Utility.ConnString);
        }
    }    
}
