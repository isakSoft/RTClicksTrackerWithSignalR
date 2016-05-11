using System;
using System.ComponentModel.DataAnnotations;

namespace com.ai.ext.upwork.test1.Models
{
    public class ClicksTracker
    {
        public int Id { get; set; }        
        public string CampaignName { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<int> Clicks { get; set; }
        public Nullable<int> Conversions { get; set; }
        public Nullable<int> Impressions { get; set; }
        public string AffiliateName { get; set; }
    }
}