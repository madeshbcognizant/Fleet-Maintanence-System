using Microsoft.EntityFrameworkCore;

namespace ScheduleService.Models
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
        }

        public DbSet<ServiceSchedule> ServiceSchedules { get; set; } = null!;
    }
}