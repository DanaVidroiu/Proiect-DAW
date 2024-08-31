using LearningPlatform.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningPlatform.Services.TagCourseService;

namespace LearningPlatform.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class TagCoursesController : ControllerBase
{
    private readonly ITagCourseService _tagCourseService;

    public TagCoursesController(ITagCourseService tagCourseService)
    {
        _tagCourseService = tagCourseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTagCourses()
    {
        var tagCourses = await _tagCourseService.GetAllTagCoursesAsync();
        return Ok(tagCourses);
    }

    [HttpGet("{courseId}/{tagId}")]
    public async Task<IActionResult> GetTagCourse(int courseId, int tagId)
    {
        var tagCourse = await _tagCourseService.GetTagCourseByIdsAsync(courseId, tagId);
        if (tagCourse == null)
            return NotFound();

        return Ok(tagCourse);
    }

    [HttpPost]
    public async Task<IActionResult> AddTagCourse([FromBody] TagCourseDto tagCourseDto)
    {
        await _tagCourseService.AddTagCourseAsync(tagCourseDto);
        return CreatedAtAction(nameof(GetTagCourse), new { courseId = tagCourseDto.CourseId, tagId = tagCourseDto.TagId }, tagCourseDto);
    }

    [HttpDelete("{courseId}/{tagId}")]
    public async Task<IActionResult> DeleteTagCourse(int courseId, int tagId)
    {
        await _tagCourseService.DeleteTagCourseAsync(courseId, tagId);
        return NoContent();
    }
}
}