using Microsoft.AspNetCore.Mvc;
using VB.API.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthorizationController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/<AuthorizationController>/5
        [HttpGet("{adminName}")]
        public IActionResult GetApiKey(string adminName)
        {
            var configAdminName = _config.GetSection("ValueBlueAdminName").Value;
            AuthorizationService.ValidateAuthenticationNameParameter(configAdminName, adminName);

            var apiKey = _config.GetSection("ApiKey").Value;

            return Ok(apiKey);
        }

        // POST api/<AuthorizationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthorizationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorizationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
