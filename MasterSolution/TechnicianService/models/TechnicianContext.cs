using Microsoft.EntityFrameworkCore;

namespace TechnicianService.models
{
    public class TechnicianContext : DbContext
    {
        public TechnicianContext(DbContextOptions<TechnicianContext> options) : base(options) 
        { 
        
        }
        public DbSet<Technician> Technicians { get; set; }  
    }
}
