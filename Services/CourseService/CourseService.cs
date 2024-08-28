using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPlatform.Dtos;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                Level = c.Level,
                Category = c.Category,
                Price = c.Price,
                IsPublished = c.IsPublished,
                ProfessorId = c.ProfessorId,
                Duration = c.Duration,
                Statistics = c.Statistics != null ? new CourseStatisticsDTO
                {
                    CourseId = c.Statistics.CourseId,
                    TotalRevenue = c.Statistics.TotalRevenue,
                    TotalEnrollments = c.Statistics.TotalEnrollments
                } : null
            }).ToListAsync();
    }

    public async Task<CourseDTO?> GetCourseByIdAsync(int id)
    {
        var course = await _context.Courses
            .Where(c => c.CourseId == id)
            .Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Description = c.Description,
                Level = c.Level,
                Category = c.Category,
                Price = c.Price,
                IsPublished = c.IsPublished,
                ProfessorId = c.ProfessorId,
                Duration = c.Duration,
                Statistics = c.Statistics != null ? new CourseStatisticsDTO
                {
                    CourseId = c.Statistics.CourseId,
                    TotalRevenue = c.Statistics.TotalRevenue,
                    TotalEnrollments = c.Statistics.TotalEnrollments
                } : null
            }).FirstOrDefaultAsync();

        return course;
    }

    public async Task<CourseDTO> CreateCourseAsync(CourseDTO courseDto)
    {
        var course = new Course
        {
            Title = courseDto.Title,
            Description = courseDto.Description,
            Level = courseDto.Level,
            Category = courseDto.Category,
            Price = courseDto.Price,
            IsPublished = courseDto.IsPublished,
            ProfessorId = courseDto.ProfessorId ?? 0,
            Duration = courseDto.Duration,
            Statistics = courseDto.Statistics != null ? new CourseStatistics
            {
                CourseId = courseDto.Statistics.CourseId,
                TotalRevenue = courseDto.Statistics.TotalRevenue,
                TotalEnrollments = courseDto.Statistics.TotalEnrollments
            } : null
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        courseDto.CourseId = course.CourseId; // set the generated ID to DTO
        return courseDto;
    }

    public async Task<CourseDTO?> UpdateCourseAsync(int id, CourseDTO courseDto)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return null;

        course.Title = courseDto.Title;
        course.Description = courseDto.Description;
        course.Level = courseDto.Level;
        course.Category = courseDto.Category;
        course.Price = courseDto.Price;
        course.IsPublished = courseDto.IsPublished;
        course.ProfessorId = courseDto.ProfessorId ?? 0;
        course.Duration = courseDto.Duration;

        if (courseDto.Statistics != null)
        {
            if (course.Statistics == null)
            {
                course.Statistics = new CourseStatistics();
            }

            course.Statistics.CourseId = courseDto.Statistics.CourseId;
            course.Statistics.TotalRevenue = courseDto.Statistics.TotalRevenue;
            course.Statistics.TotalEnrollments = courseDto.Statistics.TotalEnrollments;
        }

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        return courseDto;
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return true;
    }
}
