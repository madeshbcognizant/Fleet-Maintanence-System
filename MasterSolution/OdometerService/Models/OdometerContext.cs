using Microsoft.EntityFrameworkCore;

namespace OdometerService.Models
{
    public class OdometerContext:DbContext
    {
        public OdometerContext(DbContextOptions<OdometerContext> optionsBuilder) : base(optionsBuilder)
        {
        }
        public DbSet<Odometer> Odometers { get; set; }

    }
}
