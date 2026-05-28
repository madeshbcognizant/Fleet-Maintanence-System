using MasterService.Models;
using MasterService.Repository;

namespace MasterService.Services
{
    public class MasterService
    {
        private readonly MasterRepository _repository;

        public MasterService(MasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Master>> GetAllMastersAsync()
        {
            return await _repository.GetAllMastersAsync();
        }

        public async Task<Master?> GetMasterByTypeAsync(string type)
        {
            return await _repository.GetMasterByTypeAsync(type);
        }

        public async Task<Master?> CreateMasterAsync(Master master)
        {
            if (await _repository.ExistsAsync(master.Type))
            {
                return null; // Return null if it already exists so controller can send a Conflict status
            }

            _repository.Add(master);
            await _repository.SaveAsync();

            return master;
        }
    }
}