
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicianService.models;

namespace TechnicianService.Repository
{
    public class TechnicianRepository 
    {
        private readonly TechnicianContext _context;

        public TechnicianRepository(TechnicianContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technician>> GetAllAsync()
        {
            return await _context.Technicians.ToListAsync();
        }

        public async Task<Technician?> GetByIdAsync(int id)
        {
            return await _context.Technicians.FindAsync(id);
        }

        public async Task<Technician> AddAsync(Technician technician)
        {
            _context.Technicians.Add(technician);
            await _context.SaveChangesAsync();
            return technician;
        }

        public async Task<Technician?> UpdateAsync(Technician technician)
        {
            var existing = await _context.Technicians.FindAsync(technician.TechnicianId);
            if (existing == null) 
                return null;

            existing.TechnicianName = technician.TechnicianName;
            existing.TechnicianSkill = technician.TechnicianSkill;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Technicians.FindAsync(id);
            if (existing == null) 
                return false;

            _context.Technicians.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
