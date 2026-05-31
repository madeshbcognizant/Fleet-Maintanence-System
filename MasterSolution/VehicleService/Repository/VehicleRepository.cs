using Microsoft.EntityFrameworkCore;
using VehicleService.Models;

namespace VehicleService.Repository
{
    public class VehicleRepository
    {
        private readonly VehicleDbContext _context;

        public VehicleRepository(VehicleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleByRegistrationIdAsync(string registrationId)
        {
            return await _context.Vehicles.FindAsync(registrationId);
        }

        public async Task<Vehicle?> GetVehicleByChasisNumberAsync(string chasisNumber)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.ChasisNumber == chasisNumber);
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle?> UpdateVehicleAsync(string registrationId, Vehicle vehicle)
        {
            var existingVehicle = await _context.Vehicles.FindAsync(registrationId);
            if (existingVehicle == null)
            {
                return null;
            }

            existingVehicle.Type = vehicle.Type;
            existingVehicle.Make = vehicle.Make;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.ManfacturYear = vehicle.ManfacturYear;
            existingVehicle.ChasisNumber = vehicle.ChasisNumber;
            existingVehicle.RegistrationEndDate = vehicle.RegistrationEndDate;
            existingVehicle.PollutionCheckDate = vehicle.PollutionCheckDate;
            existingVehicle.InsuranceEndDate = vehicle.InsuranceEndDate;

            await _context.SaveChangesAsync();
            return existingVehicle;
        }

        public async Task<bool> DeleteVehicleAsync(string registrationId)
        {
            var vehicle = await _context.Vehicles.FindAsync(registrationId);
            if (vehicle == null)
            {
                return false;
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RegistrationIdExistsAsync(string registrationId)
        {
            return await _context.Vehicles.AnyAsync(v => v.RegistrationId == registrationId);
        }

        public async Task<bool> ChasisNumberExistsAsync(string chasisNumber)
        {
            return await _context.Vehicles.AnyAsync(v => v.ChasisNumber == chasisNumber);
        }
    }
}
