using LearningPlatform.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningPlatform.Services;

namespace LearningPlatform.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class CourseStatisticsController : ControllerBase
{
    private readonly ICourseStatisticsService _courseStatisticsService;

    public CourseStatisticsController(ICourseStatisticsService courseStatisticsService)
    {
        _courseStatisticsService = courseStatisticsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseStatisticsDTO>>> GetAllCourseStatistics()
    {
        var statistics = await _courseStatisticsService.GetAllCourseStatisticsAsync();
        return Ok(statistics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseStatisticsDTO>> GetCourseStatisticsById(int id)
    {
        var statistics = await _courseStatisticsService.GetCourseStatisticsByIdAsync(id);
        if (statistics == null)
        {
            return NotFound();
        }
        return Ok(statistics);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourseStatistics([FromBody] CourseStatisticsDTO courseStatisticsDto)
    {
        if (courseStatisticsDto == null)
        {
            return BadRequest();
        }

        await _courseStatisticsService.CreateCourseStatisticsAsync(courseStatisticsDto);
        return CreatedAtAction(nameof(GetCourseStatisticsById), new { id = courseStatisticsDto.CourseStatisticsId }, courseStatisticsDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourseStatistics(int id, [FromBody] CourseStatisticsDTO courseStatisticsDto)
    {
        if (courseStatisticsDto == null || courseStatisticsDto.CourseStatisticsId != id)
        {
            return BadRequest();
        }

        await _courseStatisticsService.UpdateCourseStatisticsAsync(courseStatisticsDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseStatistics(int id)
    {
        await _courseStatisticsService.DeleteCourseStatisticsAsync(id);
        return NoContent();
    }
}
}
