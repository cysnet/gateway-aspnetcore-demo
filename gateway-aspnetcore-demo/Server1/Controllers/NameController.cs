using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gateway1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameController : ControllerBase
    {
        private readonly ILogger<NameController> _logger;

        public NameController(ILogger<NameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("call server1");
            var req = Request;
            return "server1";
        }
    }
}
