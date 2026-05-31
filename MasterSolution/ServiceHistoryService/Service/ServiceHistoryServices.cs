using ServiceHistoryService.Models;
using ServiceHistoryService.Repository;

namespace ServiceHistoryService.Service
{
    public class ServiceHistoryServices
    {
        private readonly ServiceHistoryRepository _repository;
        public ServiceHistoryServices(ServiceHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ServiceHistory>> GetAllServiceHistories()
        {
            return await _repository.GetAllServiceHistories();
        }
        public async Task<ServiceHistory> AddServiceHistory(ServiceHistoryDTO serviceHistory)
        {
            int num=_repository.GetNextServiceStartNumber();
            var serviceHistoryEntity = new ServiceHistory
            {
                HistoryId = $"H{num:D5}",
                RegId = serviceHistory.RegId,
                ServiceId = serviceHistory.ServiceId,
                ServiceType = serviceHistory.ServiceType,
                TotalLabourCost = serviceHistory.TotalLabourCost,
                TotalPartsCost = serviceHistory.TotalPartsCost,
                TotalCost = serviceHistory.TotalLabourCost + serviceHistory.TotalPartsCost,
                CompletedDate = DateTime.UtcNow
            };
             return await _repository.AddServiceHistory(serviceHistoryEntity);
        }

         public async Task<ServiceHistory> GetServiceHistoryById(int id)
        {
            return await _repository.GetServiceHistoryById(id);
        }
        public async Task<ServiceHistory> DeleteHistory(int id) 
        {
            return await _repository.DeleteHistory(id);
         }

     }
}
