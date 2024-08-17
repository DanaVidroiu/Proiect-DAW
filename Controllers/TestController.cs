using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
    }
}
