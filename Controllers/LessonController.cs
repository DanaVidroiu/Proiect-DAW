using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Models;
using LearningPlatform.Dtos; 

[Route("api/[controller]")]
[ApiController]
public class LessonsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LessonsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("test-lessons")]
    public IActionResult TestLessons()
    {
        var lessons = _context.Lessons.ToList();
        if (!lessons.Any())
        {
            return Content("No lessons found in the database.");
        }

    var lessonCount = lessons.Count();
    return Content($"Number of lessons in the database: {lessonCount}");
    }

    // GET: api/Lessons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LessonDTO>>> GetLessons()
    {
        var lessons = await _context.Lessons.ToListAsync();

        if (lessons == null || !lessons.Any())
        {
            return NotFound(); // sau o listă goală dacă este necesar
        }

        var lessonDtos = lessons.Select(lesson => new LessonDTO
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            CourseId = lesson.CourseId,
            DisplayOrder = lesson.DisplayOrder
        });

        return Ok(lessonDtos);
    }


    // GET: api/Lessons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Lesson>> GetLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson == null)
        {
            return NotFound();
        }

        return lesson;
    }

    // POST: api/Lessons
    [HttpPost]
    public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson);
    }

    // PUT: api/Lessons/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLesson(int id, Lesson lesson)
    {
        if (id != lesson.Id)
        {
            return BadRequest();
        }

        _context.Entry(lesson).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LessonExists(id))
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

    // DELETE: api/Lessons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
        {
            return NotFound();
        }

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LessonExists(int id)
    {
        return _context.Lessons.Any(e => e.Id == id);
    }
}
