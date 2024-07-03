using Microsoft.AspNetCore.Mvc;
using VB.API.Logic;
using VB.API.Models;

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
            var response = new ApiKeyResponse { ApiKey = apiKey };

            return Ok(response);
        }
    }
}
