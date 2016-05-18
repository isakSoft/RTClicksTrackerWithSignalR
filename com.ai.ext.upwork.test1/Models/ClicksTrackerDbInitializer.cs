using System;
using System.Collections.Generic;
using System.Data.Entity;


namespace com.ai.ext.upwork.test1.Models
{
    public class ClicksTrackerDbInitializer : DropCreateDatabaseIfModelChanges<ClicksTrackerDbContext>
    {
        protected override void Seed(ClicksTrackerDbContext context)
        {
            //base.Seed(context);
            new List<ClicksTracker>()
            {
                new ClicksTracker { CampaignName = "InitCamp1", Date = Convert.ToDateTime(DateTime.Now), Clicks=1, Conversions=1, Impressions=1, AffiliateName="InitAffl1" },
                new ClicksTracker { CampaignName = "InitCamp2", Date = Convert.ToDateTime(DateTime.Now), Clicks = 2, Conversions = 2, Impressions = 2, AffiliateName = "InitAffl2" },
                new ClicksTracker { CampaignName = "InitCamp3", Date = Convert.ToDateTime(DateTime.Now), Clicks = 3, Conversions = 3, Impressions = 3, AffiliateName = "InitAffl3" }
            }.ForEach(clickTracker => context.ClicksTrackers.Add(clickTracker));

            context.SaveChanges();
        }
    }
}