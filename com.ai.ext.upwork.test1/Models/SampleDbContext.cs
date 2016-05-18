namespace com.ai.ext.upwork.test1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SampleDbContext : DbContext
    {
        public SampleDbContext()
            : base("name=SampleDbContext")
        {
        }

        public virtual DbSet<ClicksTracker> ClicksTrackers { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ClicksTracker>()
        //        .Property(e => e.CampaignName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<ClicksTracker>()
        //        .Property(e => e.AffiliateName)
        //        .IsUnicode(false);
        //}
    }
}
