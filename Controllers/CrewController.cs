using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MowManager.Models;

namespace MowManager.Controllers
{
    [Route("api/crew")]
    [ApiController]
    public class CrewController : ControllerBase
    {
        private readonly CrewContext _context;

        // Using dependency injection to inject in the Crew Controller
        public CrewController(CrewContext context)
        {
            _context = context;
        }

        // GET api/crew
        [HttpGet]
        public ActionResult<IEnumerable<Crew>> GetAllCrews()
        {
            return _context.CrewItems;
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