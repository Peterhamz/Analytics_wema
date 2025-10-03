using Microsoft.EntityFrameworkCore;
using System;
using Wema_Analytics.Entities;

namespace Wema_Analytics.Data
{
    public class WemaAnalyticsDbContext : DbContext
    {
        public WemaAnalyticsDbContext(DbContextOptions<WemaAnalyticsDbContext> options) : base(options) { }

        public DbSet<Branch> Branches => Set<Branch>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasIndex(b => b.Code)
                .IsUnique();
        }
    }
}
