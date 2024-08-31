using LearningPlatform.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningPlatform.Services.EnrollmentService;

namespace LearningPlatform.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnrollmentDTO>>> GetAllEnrollments()
    {
        var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentDTO>> GetEnrollmentById(int id)
    {
        var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
        if (enrollment == null)
        {
            return NotFound();
        }
        return Ok(enrollment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentDTO enrollmentDto)
    {
        if (enrollmentDto == null)
        {
            return BadRequest();
        }

        await _enrollmentService.CreateEnrollmentAsync(enrollmentDto);
        return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollmentDto.EnrollmentId }, enrollmentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] EnrollmentDTO enrollmentDto)
    {
        if (enrollmentDto == null || enrollmentDto.EnrollmentId != id)
        {
            return BadRequest();
        }

        await _enrollmentService.UpdateEnrollmentAsync(enrollmentDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrollment(int id)
    {
        await _enrollmentService.DeleteEnrollmentAsync(id);
        return NoContent();
    }
}
}