using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(com.ai.ext.upwork.test1.App_Start.Startup))]

namespace com.ai.ext.upwork.test1.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
