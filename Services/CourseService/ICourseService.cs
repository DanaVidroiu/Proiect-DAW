using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO?> GetCourseByIdAsync(int id);
        Task<CourseDTO> CreateCourseAsync(CourseDTO courseDto);
        Task<CourseDTO?> UpdateCourseAsync(int id, CourseDTO courseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<CourseDTO>> GetPublishedCoursesAsync();
        Task<IEnumerable<CoursesByLevelDTO>> GetCoursesByLevelAsync();
        Task<IEnumerable<CourseDTO>> GetCoursesWithProfessorsAsync();
        Task<IEnumerable<CourseWithLessonsDTO>> GetCoursesWithLessonsAsync();
    
    }   
}