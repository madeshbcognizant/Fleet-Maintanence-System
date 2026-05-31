using Microsoft.EntityFrameworkCore;
using MasterService.Models;

namespace MasterService.Repository
{
    public class MasterRepository
    {
        private readonly MasterContext _context;

        public MasterRepository(MasterContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Master>> GetAllMastersAsync()
        {
            return await _context.Masters.ToListAsync();
        }

        public async Task<Master?> GetMasterByTypeAsync(string type)
        {
            return await _context.Masters.FindAsync(type);
        }

        public async Task<bool> ExistsAsync(string type)
        {
            return await _context.Masters.AnyAsync(m => m.Type == type);
        }

        public void Add(Master master)
        {
            _context.Masters.Add(master);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}