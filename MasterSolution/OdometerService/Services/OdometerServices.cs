using OdometerService.Models;
using OdometerService.Repository;

namespace OdometerService.Services
{
    public class OdometerServices
    {
        private readonly OdometerRepository _repository;
        public OdometerServices(OdometerRepository repository)
        {
            _repository = repository;
        }
            public async Task<IEnumerable<Odometer>> GetAllReadings()
            {
                return await _repository.GetAllReadings();
            }
            public async Task<Odometer> AddReadings(OdometerDTO odometer)
            {
            int num=_repository.GetNextServiceStartNumber();
            var odometerEntity = new Odometer()
            {
                ReadingId = $"R{num:D5}",
                RegId = odometer.RegId,
                Current_Kilometer = odometer.Current_Kilometer,
                TimeStamp = DateTime.Now,

            };
                return await _repository.AddReadings(odometerEntity);
            }
        public async Task<IEnumerable<Odometer>> GetReadingsbyID(string id)
        {
            var readings = await _repository.GetAllReadings();
            return readings;
        }
            
    }
}
