using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPlatform.Dtos;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Services
{
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
                CourseStatisticsId = c.Statistics.CourseStatisticsId,
                CourseId = c.Statistics.CourseId, 
                TotalEnrollments = c.Statistics.TotalEnrollments,
                TotalRevenue = c.Statistics.TotalRevenue
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
                CourseStatisticsId = c.Statistics.CourseStatisticsId,
                CourseId = c.Statistics.CourseId,
                TotalEnrollments = c.Statistics.TotalEnrollments,
                TotalRevenue = c.Statistics.TotalRevenue
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
                TotalEnrollments = courseDto.Statistics.TotalEnrollments,
                TotalRevenue = courseDto.Statistics.TotalRevenue
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
            course.Statistics.TotalEnrollments = courseDto.Statistics.TotalEnrollments;
            course.Statistics.TotalRevenue = courseDto.Statistics.TotalRevenue;
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

    //filtreaza doar cursurile publicate 
    public async Task<IEnumerable<CourseDTO>> GetPublishedCoursesAsync()
    {
        return await _context.Courses
                             .Where(c => c.IsPublished) 
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
                                    CourseStatisticsId = c.Statistics.CourseStatisticsId,
                                    CourseId = c.Statistics.CourseId,
                                    TotalEnrollments = c.Statistics.TotalEnrollments,
                                    TotalRevenue = c.Statistics.TotalRevenue
                                } : null
                             })
                             .ToListAsync();
    }

    //grupeaza cursurile dupa nivel 
    public async Task<IEnumerable<CoursesByLevelDTO>> GetCoursesByLevelAsync()
    {
        return await _context.Courses
                             .Where(c => !string.IsNullOrWhiteSpace(c.Level))
                             .GroupBy(c => c.Level) 
                             .Select(group => new CoursesByLevelDTO
                             {
                                Level = group.Key, 
                                CoursesCount = group.Count(), 
                                AveragePrice = group.Average(c => c.Price)
                             })
                             .ToListAsync();
    }


    public async Task<IEnumerable<CourseDTO>> GetCoursesWithProfessorsAsync()
    {
        return await _context.Courses
                            .Join(_context.Users,
                                course => course.ProfessorId,
                                professor => professor.Id,
                                (course, professor) => new CourseDTO
                                {
                                    CourseId = course.CourseId,
                                    Title = course.Title,
                                    Description = course.Description,
                                    Level = course.Level,
                                    Category = course.Category,
                                    Price = course.Price,
                                    IsPublished = course.IsPublished,
                                    Duration = course.Duration,
                                    Statistics = course.Statistics != null ? new CourseStatisticsDTO
                                    {
                                        CourseStatisticsId = course.Statistics.CourseStatisticsId,
                                        CourseId = course.Statistics.CourseId,
                                        TotalEnrollments = course.Statistics.TotalEnrollments,
                                        TotalRevenue = course.Statistics.TotalRevenue
                                    } : null
                                })
                            .ToListAsync();
    }

    public async Task<IEnumerable<CourseWithLessonsDTO>> GetCoursesWithLessonsAsync()
    {
        return await _context.Courses
                            .Include(c => c.Lessons) 
                            .Select(c => new CourseWithLessonsDTO
                            {
                                CourseId = c.CourseId,
                                Title = c.Title,
                                Lessons = c.Lessons.Select(l => new LessonDTO
                                {
                                    LessonId = l.LessonId,
                                    Title = l.Title,
                                    Content = l.Content,
                                    DisplayOrder = l.DisplayOrder
                                }).ToList()
                            })
                            .ToListAsync();
    }

}
}