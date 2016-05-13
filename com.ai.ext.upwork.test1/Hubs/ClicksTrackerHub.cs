using Microsoft.AspNet.SignalR;
using com.ai.ext.upwork.test1.Models;
using Microsoft.AspNet.SignalR.Hubs;

namespace com.ai.ext.upwork.test1.Hubs
{
    [HubName("ClicksTrackerHub")]
    public class ClicksTrackerHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendClicks")]
        public static void SendClicks()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ClicksTrackerHub>();
            //SIGNED TO CLIENT updateClicksTracker FUNC
            context.Clients.All.updateClicksTracker();           
        }

        [HubMethodName("addClicks")]
        public static void AddClicks()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ClicksTrackerHub>();
            //SIGNED TO CLIENT updateClicksTracker FUNC
            context.Clients.All.updateClicksTracker();
        }

        [HubMethodName("updateClicks")]
        public static void UpdateClicks()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ClicksTrackerHub>();
            //SIGNED TO CLIENT updateClicksTracker FUNC
            context.Clients.All.updateClicksTracker();
        }

        [HubMethodName("deleteClicks")]
        public static void DeleteClicks()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ClicksTrackerHub>();
            //SIGNED TO CLIENT updateClicksTracker FUNC
            context.Clients.All.updateClicksTracker();
        }
    }
}