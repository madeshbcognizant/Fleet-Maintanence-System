using System.ComponentModel;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using ScheduleService.DTOs;

namespace ScheduleService.Clients
{
    public class ExternalServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ExternalServiceClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<VehicleDTO?> GetVehicleDetailsAsync(string regId)
        {
            var url = $"{_config["ServiceUrls:VehicleService"]}/api/vehicle/{regId}";
            return await _httpClient.GetFromJsonAsync<VehicleDTO>(url);
        }

        public   List<MasterDTO?> GetMasterDescriptionAsync(string type)
        {
            var url = $"{_config["ServiceUrls:MasterService"]}/api/master/{type}";
           IEnumerable<MasterDTO> list=  _httpClient.GetFromJson<List<MasterDTO>>(url);
            return list;
        }

        public async Task<OdometerDTO?> GetOdometerReadAsync(string regId)
        {
            var url = $"{_config["ServiceUrls:OdometerService"]}/api/odometer/{regId}";
            return await _httpClient.GetFromJsonAsync<OdometerDTO>(url);
        }

        public async Task<IEnumerable<TechnicianDTO>> GetAvailableTechniciansAsync()
        {
            var url = $"{_config["ServiceUrls:TechnicianService"]}/api/technician/available";
            return await _httpClient.GetFromJsonAsync<IEnumerable<TechnicianDTO>>(url) ?? Array.Empty<TechnicianDTO>();
        }

        public async Task<bool> PostScheduleAssignmentAsync(object assignmentData)
        {
            var url = $"{_config["ServiceUrls:TechnicianService"]}/api/ScheduleAssignment";
            var response = await _httpClient.PostAsJsonAsync(url, assignmentData);
            return response.IsSuccessStatusCode;
        }
    }
}
