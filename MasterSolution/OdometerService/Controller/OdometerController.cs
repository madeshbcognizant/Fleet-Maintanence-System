using Microsoft.AspNetCore.Mvc;
using OdometerService.Models;
using OdometerService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OdometerService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdometerController : ControllerBase
    {
        private readonly OdometerServices _Service;
        private readonly HttpClient _httpClient;
        public OdometerController(OdometerServices service)
        {
            _Service = service;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:0000");// port number shedule service should be replaced
        }

        // GET: api/<OdometerController>
        [HttpGet]
        public async Task<ActionResult<Odometer>> GetAllReadings()
        {
            var odometerreading = await _Service.GetAllReadings();
            return Ok(odometerreading);
           
        }

        // GET api/<OdometerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Odometer>> GetReadingsById(string id)
        {
            var odometerReading = await _Service.GetReadingsbyID(id);
            if (odometerReading == null) {
                return NotFound("reading id not exists");


            }
            return Ok(odometerReading);
        }

        // POST api/<OdometerController>
        [HttpPost]
        public async Task<ActionResult<Odometer>> Post([FromBody] OdometerDTO value)
        {
            var odometer = await _Service.AddReadings(value);
            await _httpClient.PostAsJsonAsync("api/....", odometer);//here the schedule method update api should replace

            return Ok(odometer);
        }

        //// PUT api/<OdometerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<OdometerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
