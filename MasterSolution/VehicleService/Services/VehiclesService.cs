using VehicleService.Models;
using VehicleService.Repository;

namespace VehicleService.Services
{
    public class VehiclesService
    {
        private readonly VehicleRepository _repository;

        public VehiclesService(VehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _repository.GetAllVehiclesAsync();
        }

        public async Task<Vehicle?> GetVehicleByRegistrationIdAsync(string registrationId)
        {
            if (string.IsNullOrWhiteSpace(registrationId))
            {
                return null;
            }
            return await _repository.GetVehicleByRegistrationIdAsync(registrationId);
        }

        public async Task<Vehicle?> GetVehicleByChasisNumberAsync(string chasisNumber)
        {
            if (string.IsNullOrWhiteSpace(chasisNumber) || chasisNumber.Length != 17)
            {
                return null;
            }

            return await _repository.GetVehicleByChasisNumberAsync(chasisNumber);
        }

        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            if (vehicle == null || !IsValidVehicle(vehicle))
            {
                return null;
            }

            // Check if registration ID already exists
            if (await _repository.RegistrationIdExistsAsync(vehicle.RegistrationId))
            {
                return null;
            }

            // Check if chasis number already exists
            if (await _repository.ChasisNumberExistsAsync(vehicle.ChasisNumber))
            {
                return null;
            }

            return await _repository.AddVehicleAsync(vehicle);
        }

        public async Task<Vehicle?> UpdateVehicleAsync(string registrationId, Vehicle vehicle)
        {
            if (vehicle == null || registrationId != vehicle.RegistrationId || !IsValidVehicle(vehicle))
            {
                return null;
            }

            // Check if vehicle exists
            var existingVehicle = await _repository.GetVehicleByRegistrationIdAsync(registrationId);
            if (existingVehicle == null)
            {
                return null;
            }

            // Check if chasis number is being changed and if it already exists
            if (existingVehicle.ChasisNumber != vehicle.ChasisNumber)
            {
                if (await _repository.ChasisNumberExistsAsync(vehicle.ChasisNumber))
                {
                    return null;
                }
            }

            return await _repository.UpdateVehicleAsync(registrationId, vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(string registrationId)
        {
            var vehicle = await _repository.GetVehicleByRegistrationIdAsync(registrationId);
            if (vehicle == null)
            {
                return false;
            }

            return await _repository.DeleteVehicleAsync(registrationId);
        }

        public async Task<bool> RegistrationIdExistsAsync(string registrationId)
        {
            if (string.IsNullOrWhiteSpace(registrationId))
            {
                return false;
            }
            return await _repository.RegistrationIdExistsAsync(registrationId);
        }

        public async Task<bool> ChasisNumberExistsAsync(string chasisNumber)
        {
            if (string.IsNullOrWhiteSpace(chasisNumber))
            {
                return false;
            }

            return await _repository.ChasisNumberExistsAsync(chasisNumber);
        }

        private bool IsValidVehicle(Vehicle vehicle)
        {
            if (string.IsNullOrWhiteSpace(vehicle.RegistrationId))
                return false;

            if (string.IsNullOrWhiteSpace(vehicle.Type))
                return false;

            if (string.IsNullOrWhiteSpace(vehicle.Make))
                return false;

            if (string.IsNullOrWhiteSpace(vehicle.Model))
                return false;

            if (vehicle.ManfacturYear <= 0 || vehicle.ManfacturYear > DateTime.Now.Year + 1)
                return false;

            if (string.IsNullOrWhiteSpace(vehicle.ChasisNumber) || vehicle.ChasisNumber.Length != 17)
                return false;

            if (vehicle.RegistrationEndDate <= DateTime.Now)
                return false;

            if (vehicle.PollutionCheckDate < DateTime.Now.AddYears(-1))
                return false;

            if (vehicle.InsuranceEndDate <= DateTime.Now)
                return false;

            return true;
        }
    }
}
