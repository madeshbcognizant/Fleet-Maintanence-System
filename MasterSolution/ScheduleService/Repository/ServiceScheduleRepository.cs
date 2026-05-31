using ScheduleService.Models;
using Microsoft.EntityFrameworkCore;
namespace ScheduleService.Repository
{
    public class ServiceScheduleRepository
    {
        private readonly ServiceScheduleContext _context;
        public ServiceScheduleRepository(ServiceScheduleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceSchedule>> GetServiceSchedules()
        {
            return await _context.ServiceSchedules.ToListAsync();
        }

        public async Task<ServiceSchedule?> GetServiceScheduleById(string ServiceId)
        {
            return await _context.ServiceSchedules.FindAsync(ServiceId);
        }

        public async Task<bool> ExistsAsync(string ServiceId)
        {
            return await _context.ServiceSchedules.AnyAsync(s=>s.ServiceId == ServiceId);
        }

        public async Task AddAsync(ServiceSchedule serviceSchedule)
        {
            await _context.ServiceSchedules.AddAsync(serviceSchedule);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
