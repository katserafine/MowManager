using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MowManager.Models;

namespace MowManager.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceContext _context;

        // Using dependency injection to inject in the Service Controller
        public ServiceController(ServiceContext context)
        {
            _context = context;
        }

        // GET api/service
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetAllServices()
        {
            return _context.ServiceItems;
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
