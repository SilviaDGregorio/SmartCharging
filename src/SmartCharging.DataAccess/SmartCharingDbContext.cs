using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Entities;

namespace SmartCharging.DataAccess
{
    public class SmartCharingDbContext : DbContext
    {
        public SmartCharingDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Group> Groups { get; set; }
    }
}
