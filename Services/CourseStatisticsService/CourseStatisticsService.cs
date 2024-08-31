using LearningPlatform.Models;
using LearningPlatform.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlatform.Services.CourseStatisticsService
{
public class CourseStatisticsService : ICourseStatisticsService
{
    private readonly ApplicationDbContext _context;

    public CourseStatisticsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseStatisticsDTO>> GetAllCourseStatisticsAsync()
    {
        return await _context.CourseStatistics
                             .Select(statistics => new CourseStatisticsDTO
                             {
                                 CourseStatisticsId = statistics.CourseStatisticsId,
                                 CourseId = statistics.CourseId,
                                 EnrollmentsCount = statistics.EnrollmentsCount,
                                 AverageRating = statistics.AverageRating,
                                 TotalRevenue = statistics.TotalRevenue
                             })
                             .ToListAsync();
    }

    public async Task<CourseStatisticsDTO> GetCourseStatisticsByIdAsync(int courseStatisticsId)
    {
        var statistics = await _context.CourseStatistics
                                        .Where(s => s.CourseStatisticsId == courseStatisticsId)
                                        .Select(s => new CourseStatisticsDTO
                                        {
                                            CourseStatisticsId = s.CourseStatisticsId,
                                            CourseId = s.CourseId,
                                            EnrollmentsCount = s.EnrollmentsCount,
                                            AverageRating = s.AverageRating,
                                            TotalRevenue = s.TotalRevenue
                                        })
                                        .FirstOrDefaultAsync();
        return statistics;
    }

    public async Task CreateCourseStatisticsAsync(CourseStatisticsDTO courseStatisticsDto)
    {
        var statistics = new CourseStatistics
        {
            CourseStatisticsId = courseStatisticsDto.CourseStatisticsId,
            CourseId = courseStatisticsDto.CourseId,
            EnrollmentsCount = courseStatisticsDto.EnrollmentsCount,
            AverageRating = courseStatisticsDto.AverageRating,
            TotalRevenue = courseStatisticsDto.TotalRevenue
        };
        _context.CourseStatistics.Add(statistics);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseStatisticsAsync(CourseStatisticsDTO courseStatisticsDto)
    {
        var statistics = await _context.CourseStatistics.FindAsync(courseStatisticsDto.CourseStatisticsId);
        if (statistics != null)
        {
            statistics.CourseId = courseStatisticsDto.CourseId;
            statistics.EnrollmentsCount = courseStatisticsDto.EnrollmentsCount;
            statistics.AverageRating = courseStatisticsDto.AverageRating;
            statistics.TotalRevenue = courseStatisticsDto.TotalRevenue;

            _context.CourseStatistics.Update(statistics);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCourseStatisticsAsync(int courseStatisticsId)
    {
        var statistics = await _context.CourseStatistics.FindAsync(courseStatisticsId);
        if (statistics != null)
        {
            _context.CourseStatistics.Remove(statistics);
            await _context.SaveChangesAsync();
        }
    }
}
}