using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Dtos;


namespace LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            var courseDtos = courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Level = course.Level,
                Category = course.Category,
                Price = course.Price,
                IsPublished = course.IsPublished,
                ProfessorId = course.ProfessorId,
                Duration = course.Duration
            }).ToList();

            return Ok(courseDtos);
        }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDTO>> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        var courseDto = new CourseDTO
        {
            Id = course.Id,
            Title = course.Title ?? throw new InvalidOperationException("Title cannot be null"), // Verificare pentru null
            Description = course.Description ?? throw new InvalidOperationException("Description cannot be null"), // Verificare pentru null
            Level = course.Level ?? throw new InvalidOperationException("Level cannot be null"), // Verificare pentru null
            Category = course.Category ?? throw new InvalidOperationException("Category cannot be null"), // Verificare pentru null
            Price = course.Price,
            IsPublished = course.IsPublished,
            ProfessorId = course.ProfessorId,
            Duration = course.Duration ?? throw new InvalidOperationException("Duration cannot be null") // Verificare pentru null
        };

        return Ok(courseDto);
    }


        [HttpPost]
        public async Task<ActionResult<CourseDTO>> CreateCourse(CourseDTO courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                Level = courseDto.Level,
                Category = courseDto.Category,
                Price = courseDto.Price,
                IsPublished = courseDto.IsPublished,
                ProfessorId = courseDto.ProfessorId ?? default(int),
                Duration = courseDto.Duration
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            courseDto.Id = course.Id;

            return CreatedAtAction(nameof(GetCourse), new { id = courseDto.Id }, courseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDTO courseDto)
        {
            if (id != courseDto.Id)
            {
                return BadRequest();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.Title = courseDto.Title;
            course.Description = courseDto.Description;
            course.Level = courseDto.Level;
            course.Category = courseDto.Category;
            course.Price = courseDto.Price;
            course.IsPublished = courseDto.IsPublished;
            course.ProfessorId = courseDto.ProfessorId ?? default(int);
            course.Duration = courseDto.Duration;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
