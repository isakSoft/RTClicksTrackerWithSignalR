using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Web;

namespace com.ai.ext.upwork.test1.Models
{
    public class ClicksTrackerRepository
    {
        static IEnumerable<ClicksTracker> _clicks = new ConcurrentBag<ClicksTracker>();

        public ClicksTrackerRepository()
        {
            
        }

        public IEnumerable<ClicksTracker> GetAll()
        {
            string selectQuery = "SELECT [CampaignName], [Date], [Clicks], [Conversions], [Impressions], [AffiliateName] FROM[dbo].[DevTest]";
            _clicks = Utility.GetAllClicks(selectQuery);
            return _clicks;
        }

        /*
        public void Add(ClicksTracker item)
        {            
            _clicks.Add(item);
        }

        public ClicksTracker Find(string key)
        {
            ClicksTracker item;
            _clicks.TryPeek(out item);
            return item;
        }

        public ClicksTracker Remove(string key)
        {
            ClicksTracker item;
            _clicks.TryTake(out item);
            return item;
        }

        public void Update(ClicksTracker item)
        {            
            _clicks.TryPeek(out item);            
        }
        */
    }
}