using Microsoft.EntityFrameworkCore; 

namespace MasterService.Models
{
    public class MasterContext : DbContext
    {
        // Standard constructor required to pass configuration (like connection strings) from Program.cs
        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {
        }

        // This represents the database table for your Master model
        public DbSet<Master> Masters { get; set; } = null!;
    }
}
