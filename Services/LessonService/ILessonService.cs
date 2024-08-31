using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.Services.LessonService
{
public interface ILessonService
{
    Task<IEnumerable<LessonDTO>> GetAllLessonsAsync();
    Task<LessonDTO> GetLessonByIdAsync(int lessonId);
    Task CreateLessonAsync(LessonDTO lessonDto);
    Task UpdateLessonAsync(LessonDTO lessonDto);
    Task DeleteLessonAsync(int lessonId);
}
}
