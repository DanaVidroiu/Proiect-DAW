using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            return await _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .ToListAsync();
        }

        // GET: api/Enrollment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        // POST: api/Enrollment
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            // Verifică dacă UserId și CourseId există
            var userExists = await _context.Users.AnyAsync(u => u.Id == enrollment.UserId);
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == enrollment.CourseId);

            if (!userExists || !courseExists)
            {
                return BadRequest("UserId or CourseId is invalid.");
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, enrollment);
        }


        // PUT: api/Enrollment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return BadRequest();
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
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

        // DELETE: api/Enrollment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
