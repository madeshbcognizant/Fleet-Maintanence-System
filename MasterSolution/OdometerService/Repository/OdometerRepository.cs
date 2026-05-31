using Microsoft.EntityFrameworkCore;
using OdometerService.Models;

namespace OdometerService.Repository
{
    public class OdometerRepository
    {
        private readonly OdometerContext _context;
        public OdometerRepository(OdometerContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Odometer>> GetAllReadings() { 
        return await _context.Odometers.ToListAsync();
        }
        public async Task<Odometer> AddReadings(Odometer odometer)
        {
              _context.Odometers.Add(odometer);
            await _context.SaveChangesAsync();
            return odometer;
        }
        public async Task<Odometer> GetReadingByID(string readingID)
        {
            var readings=await _context.Odometers.FindAsync(readingID);
            return readings;
        }
        public int GetNextServiceStartNumber()
        {
            var lastServiceId = _context.Odometers
                .OrderByDescending(s => s.ReadingId)
                .Select(s =>s.ReadingId)
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
