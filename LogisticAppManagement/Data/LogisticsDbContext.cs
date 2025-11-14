using LogisticAppManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticAppManagement.Data
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet <User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Driver)
                .WithMany(del => del.Deliveries)
                .HasForeignKey(del => del.DriverId);
        }
    }
}
