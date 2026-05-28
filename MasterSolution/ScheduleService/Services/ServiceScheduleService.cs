using ScheduleService.Clients;
using ScheduleService.DTOs;
using ScheduleService.Models;
using ScheduleService.Repository; // Make sure your folder is named Repository or Repositories

namespace ScheduleService.Services
{
    public class ServiceScheduleService
    {
        private readonly ServiceScheduleRepository _repository;
        private readonly ExternalServiceClient _client;

        public ServiceScheduleService(ServiceScheduleRepository repository, ExternalServiceClient client)
        {
            _repository = repository;
            _client = client;
        }

        // 1. Get All Records (Calls your repository method)
        public async Task<IEnumerable<ServiceSchedule>> GetAllSchedulesAsync()
        {
            // Note: If your repository methods are named GetAllAsync(), change this to _repository.GetAllAsync()
            return await _repository.GetServiceSchedules();
        }

        // 2. Get Single Record by ID
        public async Task<ServiceSchedule?> GetScheduleByIdAsync(string serviceId)
        {
            // Note: If your repository methods are named GetByIdAsync(), change this to _repository.GetByIdAsync(serviceId)
            return await _repository.GetServiceScheduleById(serviceId);
        }

        // 3. Manual Create Schedule Endpoint
        public async Task<ServiceSchedule> CreateScheduleAsync(ServiceSchedule schedule)
        {
            if (await _repository.ExistsAsync(schedule.ServiceId))
            {
                return null!;
            }

            // FIXED: If the status is empty, make it default to "Pending"
            if (string.IsNullOrEmpty(schedule.Status))
            {
                schedule.Status = "Pending";
            }

            await _repository.AddAsync(schedule);
            await _repository.SaveAsync();

            return schedule;
        }

        // 4. Automatic compilation from external microservices
        public async Task<ServiceSchedule?> TriggerAndGenerateScheduleAsync(string regId, string serviceType, string nameOfService)
        {
            // Fetch data from external microservices concurrently
            var vehicleTask = _client.GetVehicleDetailsAsync(regId);
            var masterTask = _client.GetMasterDescriptionAsync(serviceType);
            var odometerTask = _client.GetOdometerReadAsync(regId);

            await Task.WhenAll(vehicleTask, masterTask, odometerTask);

            var vehicle = await vehicleTask;
            var master = await masterTask;
            var odometer = await odometerTask;

            if (vehicle == null) return null; // Safety Validation

            // Map and generate automatic operational details
            var newSchedule = new ServiceSchedule
            {
                ServiceId = "SRV-" + Guid.NewGuid().ToString()[..8].ToUpper(),
                RegId = vehicle.RegId,
                Type = vehicle.Type,
                Model = vehicle.Model,
                NameOfService = nameOfService,
                Description = master?.Description ?? "No description available",
                ScheduleDate = DateTime.UtcNow, // Automated time stamp
                Kilometer = odometer?.CurrentKilometer ?? 0,
                Status = "Pending"
            };

            // FIXED: Changed from _repo to _repository
            await _repository.AddAsync(newSchedule);
            await _repository.SaveAsync();

            return newSchedule;
        }

        // 5. Fleet Manager interaction workflow mapping
        public async Task<bool> AssignTechnicianAsync(string serviceId, string technicianId)
        {
            // FIXED: Changed from _repo to _repository and aligned with your method name
            var schedule = await _repository.GetServiceScheduleById(serviceId);
            if (schedule == null) return false;

            // Compile the exact payload needed for the Technician service table
            var assignmentPayload = new
            {
                ServiceId = schedule.ServiceId,
                TechnicianId = technicianId,
                RegId = schedule.RegId,
                NameOfService = schedule.NameOfService,
                Description = schedule.Description,
                ScheduleDate = schedule.ScheduleDate,
                Km = schedule.Kilometer,
                Status = "Assigned"
            };

            // Forward to Technician microservice
            var isPosted = await _client.PostScheduleAssignmentAsync(assignmentPayload);

            if (isPosted)
            {
                // Update local status to Assigned
                schedule.Status = "Assigned";
                await _repository.SaveAsync();
                return true;
            }

            return false;
        }

        // 6. Gets available technicians for dropdown mapping selection 
        public async Task<IEnumerable<TechnicianDTO>> GetTechniciansForDropdownAsync()
        {
            return await _client.GetAvailableTechniciansAsync();
        }
    }
}