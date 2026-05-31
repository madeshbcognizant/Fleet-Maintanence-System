using ScheduleAssignmentService.Models;
using ScheduleAssignmentService.Repository;

namespace ScheduleAssignmentService.Services
{
    public class ScheduleAssignmentServices
    {
        private readonly Schedule_Assignment_Repository _repository;
        public ScheduleAssignmentServices(Schedule_Assignment_Repository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Schedule_Assignment>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<Schedule_Assignment> GetById(string id)
        {
            return await _repository.GetById(id);
        }
        public async Task<Schedule_Assignment> Add(Schedule_Assignment_DTO scheduleAssignment)
        {
            int start = _repository.GetNextServiceStartNumber();
            var scheduleAssignmentEntity = new Schedule_Assignment
            {
                AssignmentId = "A" + start.ToString("D4"),
                ServiceId = scheduleAssignment.ServiceId,
                TechnicianId = scheduleAssignment.TechnicianId,
                RegId = scheduleAssignment.RegId,
                NameOfService = scheduleAssignment.NameOfService,
                Description = scheduleAssignment.Description,
                ScheduleDate = scheduleAssignment.ScheduleDate,
                Km = scheduleAssignment.Km,
                Status = scheduleAssignment.Status
            };
            return await _repository.Add(scheduleAssignmentEntity);
        }
        public async Task<Schedule_Assignment> Update(Schedule_Assignment scheduleAssignment)
        {
            return await _repository.Update(scheduleAssignment);


        }
        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
        public async Task<string> CompleteService(string id)
        {
            return await _repository.CompleteService(id);
        }
    }
}
