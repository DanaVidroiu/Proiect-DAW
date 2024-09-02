using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireProfessorRole")]
    public class ProfessorController : ControllerBase
    {
        [HttpGet("professor-data")]
        public IActionResult GetProfessorData()
        {
            return Ok("This is data only accessible by Professors.");
        }
    }
}