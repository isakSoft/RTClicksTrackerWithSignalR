using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Web;

namespace com.ai.ext.upwork.test1.Models
{
    public class ClicksTrackerRepository
    {
        static List<ClicksTracker> _clicks = new List<ClicksTracker>();

        public ClicksTrackerRepository()
        {

        }

        public IEnumerable<ClicksTracker> GetAll()
        {
            string selectQuery = "SELECT [ID], [CampaignName], [Date], [Clicks], [Conversions], [Impressions], [AffiliateName] FROM[dbo].[DevTest]";
            _clicks = Utility.GetAllClicks(selectQuery);
            return _clicks;
        }


        public void Add(ClicksTracker item)
        {
            if (Utility.AddClickToDB(item))
            {
                _clicks.Add(item);
            }
        }

        public ClicksTracker Find(int Id)
        {
            string selectQuery = "SELECT  [ID], [CampaignName], [Date], [Clicks], [Conversions], [Impressions], [AffiliateName] FROM[dbo].[DevTest] WHERE [Id]=" + Id;
            _clicks = Utility.GetAllClicks(selectQuery);
            ClicksTracker item = _clicks[0];
            return item;
        }
        /*
        public ClicksTracker Remove(string key)
        {
            ClicksTracker item;
            _clicks.TryTake(out item);
            return item;
        }
        */
        public bool Update(ClicksTracker item)
        {
            if (Utility.UpdateClickToDB(item))
            {
                return true;
            }
            return false;
        }

    }
}