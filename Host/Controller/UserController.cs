using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Host.Controller
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        [Route("info")]
        public IActionResult GetUser()
        {
            _logger.LogInformation($"Getting user information for {User?.Identity?.Name}");
            var user = new { name = User.Identity.Name };

            return new JsonResult(user);
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            _logger.LogInformation($"Logging out user {User?.Identity?.Name}");
            return SignOut("cookies", "oidc");
        }
    }
}