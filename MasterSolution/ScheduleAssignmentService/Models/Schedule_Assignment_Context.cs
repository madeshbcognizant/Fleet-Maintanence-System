using Microsoft.EntityFrameworkCore;
namespace ScheduleAssignmentService.Models
{
    public class Schedule_Assignment_Context : DbContext
    {
        public Schedule_Assignment_Context(DbContextOptions<Schedule_Assignment_Context> options) : base(options)
        {
        }
        public DbSet<Schedule_Assignment> Schedule_Assignments { get; set; }
    }
}
