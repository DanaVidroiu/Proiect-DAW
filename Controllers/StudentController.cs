using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireStudentRole")]
    public class StudentController : ControllerBase
    {
        [HttpGet("student-data")]
        public IActionResult GetStudentData()
        {
            return Ok("This is data only accessible by Students.");
        }
    }
}
