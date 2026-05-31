using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScheduleAssignmentService.Models;

namespace ScheduleAssignmentService.Repository
{
    public class Schedule_Assignment_Repository
    {
        private readonly Schedule_Assignment_Context _context;

        public Schedule_Assignment_Repository(Schedule_Assignment_Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Schedule_Assignment>> GetAll()
        {
            return await Task.FromResult(_context.Schedule_Assignments.ToList());
        }
        public async Task<Schedule_Assignment> Add(Schedule_Assignment scheduleAssignment)
        {
            _context.Schedule_Assignments.Add(scheduleAssignment);
            await _context.SaveChangesAsync();
            return scheduleAssignment;
        }
        public async Task<Schedule_Assignment> GetById(string id)
        {
            var assignment = await _context.Schedule_Assignments.FindAsync(id);
            return assignment;
        }
        public async Task<Schedule_Assignment> Update(Schedule_Assignment scheduleAssignment)
        {
            var existingAssignment = await _context.Schedule_Assignments.FindAsync(scheduleAssignment.AssignmentId);
            if (existingAssignment != null)
            {
                _context.Entry(existingAssignment).CurrentValues.SetValues(scheduleAssignment);
                await _context.SaveChangesAsync(); return scheduleAssignment;
            }
            return existingAssignment;
        }
        public async Task<bool> Delete(string id)
        {
            var assignment = await _context.Schedule_Assignments.FindAsync(id);
            if (assignment != null)
            {
                _context.Schedule_Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<string> CompleteService(string id)
        {
            var assignment = await _context.Schedule_Assignments.FindAsync(id);
            if (assignment == null)
            {
                return "service is not found";
            }
            assignment.Status = "Completed";
            await _context.SaveChangesAsync();
            return "service is completed";
        }
        public int GetNextServiceStartNumber()
        {
            var lastServiceId = _context.Schedule_Assignments
                .OrderByDescending(s => s.AssignmentId)
                .Select(s => s.AssignmentId)
                .FirstOrDefault();

            int startNumber = 1;

            if (!string.IsNullOrEmpty(lastServiceId))
            {
                if (int.TryParse(lastServiceId.Substring(1), out int lastNumber))
                {
                    startNumber = lastNumber + 1;
                }
            }

            return startNumber;
        }


    }
}
       

