using Microsoft.EntityFrameworkCore;

namespace VehicleService.Models
{
    public class VehicleDbContext:DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
