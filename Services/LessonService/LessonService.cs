using LearningPlatform.Models;
using LearningPlatform.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlatform.Services.LessonService
{
public class LessonService : ILessonService
{
    private readonly ApplicationDbContext _context;

    public LessonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LessonDTO>> GetAllLessonsAsync()
    {
        return await _context.Lessons
                             .Select(lesson => new LessonDTO
                             {
                                 LessonId = lesson.LessonId,
                                 Title = lesson.Title,
                                 Content = lesson.Content,
                                 CourseId = lesson.CourseId,
                                 DisplayOrder = lesson.DisplayOrder
                             })
                             .ToListAsync();
    }

    public async Task<LessonDTO> GetLessonByIdAsync(int lessonId)
    {
        var lesson = await _context.Lessons
                                   .Where(l => l.LessonId == lessonId)
                                   .Select(l => new LessonDTO
                                   {
                                       LessonId = l.LessonId,
                                       Title = l.Title,
                                       Content = l.Content,
                                       CourseId = l.CourseId,
                                       DisplayOrder = l.DisplayOrder
                                   })
                                   .FirstOrDefaultAsync();
        return lesson;
    }

    public async Task CreateLessonAsync(LessonDTO lessonDto)
    {
        var lesson = new Lesson
        {
            LessonId = lessonDto.LessonId,
            Title = lessonDto.Title,
            Content = lessonDto.Content,
            CourseId = lessonDto.CourseId,
            DisplayOrder = lessonDto.DisplayOrder
        };
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLessonAsync(LessonDTO lessonDto)
    {
        var lesson = await _context.Lessons.FindAsync(lessonDto.LessonId);
        if (lesson != null)
        {
            lesson.Title = lessonDto.Title;
            lesson.Content = lessonDto.Content;
            lesson.CourseId = lessonDto.CourseId;
            lesson.DisplayOrder = lessonDto.DisplayOrder;

            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteLessonAsync(int lessonId)
    {
        var lesson = await _context.Lessons.FindAsync(lessonId);
        if (lesson != null)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }
    }
}
}
