using Microsoft.EntityFrameworkCore;

namespace ServiceHistoryService.Models
{
    public class ServiceHistoryContext:DbContext
    {

        public ServiceHistoryContext(DbContextOptions<ServiceHistoryContext> optionsBuilder) : base(optionsBuilder)
        {
        }
        public DbSet<ServiceHistory> ServiceHistories { get; set; }
    }
}
