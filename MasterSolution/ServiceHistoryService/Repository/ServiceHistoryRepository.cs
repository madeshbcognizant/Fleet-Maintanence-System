using Microsoft.EntityFrameworkCore;
using ServiceHistoryService.Models;

namespace ServiceHistoryService.Repository
{
    public class ServiceHistoryRepository
    {
        private readonly ServiceHistoryContext _context;
        public ServiceHistoryRepository(ServiceHistoryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ServiceHistory>> GetAllServiceHistories()
        {
            return await _context.ServiceHistories.ToListAsync();
        }
        public async Task<ServiceHistory> AddServiceHistory(ServiceHistory serviceHistory)
        {
            _context.ServiceHistories.Add(serviceHistory);
            await _context.SaveChangesAsync();
            return serviceHistory;
        }
        public async Task<ServiceHistory> GetServiceHistoryById(int id)
        {
            return await _context.ServiceHistories.FindAsync(id);
        }
        public async Task<ServiceHistory> DeleteHistory(int id) {
        {
            var serviceHistory = await _context.ServiceHistories.FindAsync(id);
            if (serviceHistory != null)
            {
                _context.ServiceHistories.Remove(serviceHistory);
                await _context.SaveChangesAsync();
            }
            return serviceHistory;
        }
        public int GetNextServiceStartNumber()
        {
            var lastServiceId = _context.ServiceHistories
                .OrderByDescending(s => s.HistoryId)
                .Select(s => s.HistoryId)
                .FirstOrDefault();

            int startNumber = 1;

            if (!string.IsNullOrEmpty(lastServiceId))
            {
                if (int.TryParse(lastServiceId.Substring(1), out int lastNumber))
                {
                    startNumber = lastNumber + 1;
                }
            }

            return startNumber;
        }
    }
}
