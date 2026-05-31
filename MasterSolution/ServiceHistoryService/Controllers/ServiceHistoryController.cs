using Microsoft.AspNetCore.Mvc;
using ServiceHistoryService.Models;
using ServiceHistoryService.Repository;
using ServiceHistoryService.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceHistoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly ServiceHistoryServices _service;
        private readonly HttpClient _httpClientSchedule;
        private readonly HttpClient _httpClientAssignment;
        public ServiceHistoryController(ServiceHistoryServices service)
        {
            _service = service;
            _httpClientSchedule = new HttpClient();
            _httpClientSchedule.BaseAddress = new Uri("http://localhost:0000");// port number schedule service should be replaced
            _httpClientAssignment = new HttpClient();
            _httpClientAssignment.BaseAddress = new Uri("http://localhost:0000");// port number assignment service should be replaced
        }
        // GET: api/<ServiceHistoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceHistory>>> Get()
        {
            var serviceHistories = await _service.GetAllServiceHistories();
            return Ok(serviceHistories);
        }

        // GET api/<ServiceHistoryController>/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<ServiceHistory>> Get(int id)
        {
            var serviceHistory = await _service.GetServiceHistoryById(id);
            if (serviceHistory == null)
            {
                return NotFound();
            }
            return Ok(serviceHistory);
        }

        // POST api/<ServiceHistoryController>
        [HttpPost]
        public async Task<ActionResult<ServiceHistory>> Post([FromBody] ServiceHistoryDTO serviceHistory)
        {
            if(serviceHistory == null)
            {
                return BadRequest();
            }
            var createdServiceHistory = await _service.AddServiceHistory(serviceHistory);
            await _httpClientSchedule.PostAsJsonAsync("/api/schedule", serviceHistory.ServiceId);//method and endpoint should be replaced according to schedule service for make status completed
            await _httpClientAssignment.PostAsJsonAsync("/api/assignment", serviceHistory.ServiceId);//method and endpoint should be replaced according to assignment service for make status completed
            return CreatedAtAction(nameof(Get), new { id = createdServiceHistory.HistoryId }, createdServiceHistory);
        }

        //// PUT api/<ServiceHistoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ServiceHistoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceHistory>> Delete(int id)
        {
            var deletedServiceHistory = await _service.DeleteHistory(id);
            if (deletedServiceHistory == null)
            {
                return NotFound();
            }
            return Ok(deletedServiceHistory);
        }
    }
}
