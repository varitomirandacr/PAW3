using Microsoft.AspNetCore.Mvc;
using PAW3.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PAW3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempDataController : ControllerBase
    {
        // GET: api/<TempDataController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return TempData.GetData();
        }

        // GET api/<TempDataController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TempDataController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TempDataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TempDataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
