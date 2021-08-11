using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Api
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        public IActionResult Get()
        {
            _logger.LogDebug("Request received");
            return new JsonResult("OK");
        }

        /// <summary>
        /// Retrieves all Claim Types and Values from the Identity token
        /// </summary>
        /// <returns></returns>
        [Route("identity")]
        [Authorize]
        public IActionResult Identity()
        {
            _logger.LogInformation($"Building claims list for {User.Claims.FirstOrDefault(x => x.Type.Equals("sid"))}");
            var claims = from c in User.Claims select new { c.Type, c.Value };

            return new JsonResult(claims);
        }
    }
}