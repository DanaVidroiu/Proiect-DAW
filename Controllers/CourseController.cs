using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Dtos;
using LearningPlatform.Services;

namespace LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse(CourseDTO courseDto)
        {
            var createdCourse = await _courseService.CreateCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.CourseId }, createdCourse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDTO courseDto)
        {
            var updated = await _courseService.UpdateCourseAsync(id, courseDto);
            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var deleted = await _courseService.DeleteCourseAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("published-courses")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetPublishedCourses()
        {
            var publishedCourses = await _courseService.GetPublishedCoursesAsync();
            return Ok(publishedCourses);
        }


        [HttpGet("courses-by-level")]
        public async Task<ActionResult<IEnumerable<CoursesByLevelDTO>>> GetCoursesByLevel()
        {
            var coursesByLevel = await _courseService.GetCoursesByLevelAsync();
            return Ok(coursesByLevel);
        }

        [HttpGet("courses-with-professors")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCoursesWithProfessors()
        {
            var coursesWithProfessors = await _courseService.GetCoursesWithProfessorsAsync();
            return Ok(coursesWithProfessors);
        }

        [HttpGet("courses-with-lessons")]
        public async Task<ActionResult<IEnumerable<CourseWithLessonsDTO>>> GetCoursesWithLessons()
        {
            var coursesWithLessons = await _courseService.GetCoursesWithLessonsAsync();
            return Ok(coursesWithLessons);
        }
}
}