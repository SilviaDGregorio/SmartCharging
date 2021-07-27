using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Entities;

namespace SmartCharging.DataAccess
{
    public class SmartCharingDbContext : DbContext
    {
        public SmartCharingDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ChargeStation> ChargeStation { get; set; }
        public DbSet<Connector> Connector { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connector>()
                .Property(b => b.Active)
                .HasDefaultValue(1);
            modelBuilder.Entity<Connector>()
            .HasKey(c => new { c.Id, c.ChargeStationId });
        }
    }
}
