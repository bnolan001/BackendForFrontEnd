using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the name of the Authenticated user
        /// </summary>
        /// <returns>If the user is not authenticated then the <c>name</c> property will be null</returns>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation($"Getting user details for {User?.Identity?.Name}");
            var user = User?.Identity;
            return Ok(new
            {
                name = user?.Name,
            }); ;
        }
    }
}