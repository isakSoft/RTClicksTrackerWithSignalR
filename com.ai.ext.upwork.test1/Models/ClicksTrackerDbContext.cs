namespace com.ai.ext.upwork.test1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClicksTrackerDbContext : DbContext
    {
        public ClicksTrackerDbContext()
            : base("name=ClicksTrackerDbContext")
        {
        }

        public virtual DbSet<ClicksTracker> ClicksTrackers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClicksTracker>()
                .Property(e => e.CampaignName)
                .IsUnicode(false);

            modelBuilder.Entity<ClicksTracker>()
                .Property(e => e.AffiliateName)
                .IsUnicode(false);
        }
    }
}
