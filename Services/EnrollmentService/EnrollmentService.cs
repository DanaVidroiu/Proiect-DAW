using LearningPlatform.Models;
using LearningPlatform.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EnrollmentService : IEnrollmentService
{
    private readonly ApplicationDbContext _context;

    public EnrollmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EnrollmentDTO>> GetAllEnrollmentsAsync()
    {
        return await _context.Enrollments
                             .Select(enrollment => new EnrollmentDTO
                             {
                                 EnrollmentId = enrollment.EnrollmentId,
                                 UserId = enrollment.UserId,
                                 CourseId = enrollment.CourseId,
                                 EnrollmentDate = enrollment.EnrollmentDate
                             })
                             .ToListAsync();
    }

    public async Task<EnrollmentDTO> GetEnrollmentByIdAsync(int enrollmentId)
    {
        var enrollment = await _context.Enrollments
                                       .Where(e => e.EnrollmentId == enrollmentId)
                                       .Select(e => new EnrollmentDTO
                                       {
                                           EnrollmentId = e.EnrollmentId,
                                           UserId = e.UserId,
                                           CourseId = e.CourseId,
                                           EnrollmentDate = e.EnrollmentDate
                                       })
                                       .FirstOrDefaultAsync();
        return enrollment;
    }

    public async Task CreateEnrollmentAsync(EnrollmentDTO enrollmentDto)
    {
        var enrollment = new Enrollment
        {
            EnrollmentId = enrollmentDto.EnrollmentId,
            UserId = enrollmentDto.UserId,
            CourseId = enrollmentDto.CourseId,
            EnrollmentDate = enrollmentDto.EnrollmentDate
        };
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEnrollmentAsync(EnrollmentDTO enrollmentDto)
    {
        var enrollment = await _context.Enrollments.FindAsync(enrollmentDto.EnrollmentId);
        if (enrollment != null)
        {
            enrollment.UserId = enrollmentDto.UserId;
            enrollment.CourseId = enrollmentDto.CourseId;
            enrollment.EnrollmentDate = enrollmentDto.EnrollmentDate;

            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteEnrollmentAsync(int enrollmentId)
    {
        var enrollment = await _context.Enrollments.FindAsync(enrollmentId);
        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }
    }
}
