using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.Services.CourseStatisticsService
{
    public interface ICourseStatisticsService
    {
        Task<IEnumerable<CourseStatisticsDTO>> GetAllCourseStatisticsAsync();
        Task<CourseStatisticsDTO> GetCourseStatisticsByIdAsync(int courseStatisticsId);
        Task CreateCourseStatisticsAsync(CourseStatisticsDTO courseStatisticsDto);
        Task UpdateCourseStatisticsAsync(CourseStatisticsDTO courseStatisticsDto);
        Task DeleteCourseStatisticsAsync(int courseStatisticsId);
    }
}
