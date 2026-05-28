using Microsoft.AspNetCore.Mvc;
using ScheduleAssignmentService.Models;
using ScheduleAssignmentService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleAssignmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleAssignmentController : ControllerBase
    {
        private readonly ScheduleAssignmentServices _service;
        public ScheduleAssignmentController(ScheduleAssignmentServices service)
        {
            _service = service;
        }
        // GET: api/<ScheduleAssignmentController>
        [HttpGet]
        public async Task<IEnumerable<Schedule_Assignment>> Get()
        {
            var assignment= await _service.GetAll();
            return assignment;
        }

        // GET api/<ScheduleAssignmentController>/5
        [HttpGet("{id}")]
        public async Task<Schedule_Assignment> Get(string id)
        {
            return await _service.GetById(id);
        }

        // POST api/<ScheduleAssignmentController>
        [HttpPost]
        public async Task<Schedule_Assignment> Post([FromBody] Schedule_Assignment_DTO scheduleAssignment)
        {
            return await _service.Add(scheduleAssignment);
        }

        // PUT api/<ScheduleAssignmentController>/5
        [HttpPut("{id}")]
        public async Task<Schedule_Assignment> Put(string id, [FromBody] Schedule_Assignment scheduleAssignment)
        {
            scheduleAssignment.AssignmentId = id;
            return await _service.Update(scheduleAssignment);
        }

        // DELETE api/<ScheduleAssignmentController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            return await _service.Delete(id);
        }
        
        // PUT api/<ScheduleAssignmentController>/5/complete
        [HttpPut("{id}/complete")]
        public async Task<string> CompleteService(string id)
        {
            return await _service.CompleteService(id);
        }
    }
}
