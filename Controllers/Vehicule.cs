using Microsoft.AspNetCore.Mvc;
using InterventionAPI.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterventionAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]")]
    [ApiController]
    public class BaniolaController : ControllerBase
    {
#pragma warning disable IDE0052 // Supprimer les membres privés non lus
        private readonly Learn_DBContext context;
#pragma warning restore IDE0052 // Supprimer les membres privés non lus
        public BaniolaController(Learn_DBContext learn_DB)
        {
            context = learn_DB;
        }

       
        // GET par code: api/<BaniolaController>
        [HttpGet("{Code}")]
        public string Get(int Code)
        {
            return "value";
        }

        // POST api/<BaniolaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT par code: api/<BaniolaController>
        [HttpPut("{Code}")]
        public void Put(int Code, [FromBody] string value)
        {
        }

        // DELETE api/<BaniolaController>/5
        [HttpDelete("{Code}")]
        public void Delete(int Code)
        {
        }
    }
}
