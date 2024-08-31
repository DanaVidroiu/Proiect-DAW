using LearningPlatform.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningPlatform.Services.LessonService;

namespace LearningPlatform.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LessonDTO>>> GetAllLessons()
    {
        var lessons = await _lessonService.GetAllLessonsAsync();
        return Ok(lessons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDTO>> GetLessonById(int id)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(id);
        if (lesson == null)
        {
            return NotFound();
        }
        return Ok(lesson);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson([FromBody] LessonDTO lessonDto)
    {
        if (lessonDto == null)
        {
            return BadRequest();
        }

        await _lessonService.CreateLessonAsync(lessonDto);
        return CreatedAtAction(nameof(GetLessonById), new { id = lessonDto.LessonId }, lessonDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonDTO lessonDto)
    {
        if (lessonDto == null || lessonDto.LessonId != id)
        {
            return BadRequest();
        }

        await _lessonService.UpdateLessonAsync(lessonDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        await _lessonService.DeleteLessonAsync(id);
        return NoContent();
    }
}
}