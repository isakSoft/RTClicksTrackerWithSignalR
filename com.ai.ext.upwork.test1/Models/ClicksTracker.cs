using System;
namespace com.ai.ext.upwork.test1.Models
{
    public class ClicksTracker
    {
        public int Id { get; set; }
        public string CampaignName { get; set; }
        public DateTime Date { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public int Impressions { get; set; }
        public string AffiliateName { get; set; }
    }
}