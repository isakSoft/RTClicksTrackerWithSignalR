using System;
using System.Collections.Generic;
using System.Data.Entity;

using com.ai.ext.upwork.test1.Hubs;

namespace com.ai.ext.upwork.test1.Models
{
    public class ClicksTrackerRepository : IRepository
    {
        private ClicksTrackerDbContext context = new ClicksTrackerDbContext();

        public IEnumerable<ClicksTracker> ClicksTrackers
        {
            get{ return context.ClicksTrackers; }
        }

        //This implements both ADD & UPDATE functionalities
        public int SaveClicksTracker(ClicksTracker item)
        {
            if(item.ID == 0) { //ADD record to DB
                context.ClicksTrackers.Add(item);
            }
            else
            {
                //UPDATE record to DB
                ClicksTracker DbEntry = context.ClicksTrackers.Find(item.ID);
                if(DbEntry == null)
                {
                    DbEntry.CampaignName = item.CampaignName;
                    DbEntry.Date = Convert.ToDateTime(DateTime.Now);
                    DbEntry.Clicks = item.Clicks;
                    DbEntry.Conversions = item.Conversions;
                    DbEntry.Impressions = item.Impressions;
                    DbEntry.AffiliateName = item.AffiliateName;

                    if(context.Entry(DbEntry).State == EntityState.Modified)
                    {
                        ClicksTrackerHub.UpdateClicks();
                    }
                }               
            }
            context.SaveChanges();
            ClicksTrackerHub.AddClicks();
            return 1;
        }
        
        public ClicksTracker DeleteClicksTracker(int itemId)
        {
            ClicksTracker DbEntry = context.ClicksTrackers.Find(itemId);
            if(DbEntry != null)
            {
                context.ClicksTrackers.Remove(DbEntry);         
            }
            context.SaveChangesAsync();
            ClicksTrackerHub.DeleteClicks();
            return DbEntry;
        }
    }
}