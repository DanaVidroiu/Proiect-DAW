using LearningPlatform.Models;
using LearningPlatform.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlatform.Services.TagCourseService
{
public class TagCourseService : ITagCourseService
{
    private readonly ApplicationDbContext _context;

    public TagCourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TagCourseDto>> GetAllTagCoursesAsync()
    {
        return await _context.TagCourses
            .Select(tc => new TagCourseDto
            {
                CourseId = tc.CourseId,
                TagId = tc.TagId
            }).ToListAsync();
    }

    public async Task<TagCourseDto> GetTagCourseByIdsAsync(int courseId, int tagId)
    {
        var tagCourse = await _context.TagCourses
            .FirstOrDefaultAsync(tc => tc.CourseId == courseId && tc.TagId == tagId);
        
        if (tagCourse == null)
            return null;

        return new TagCourseDto
        {
            CourseId = tagCourse.CourseId,
            TagId = tagCourse.TagId
        };
    }

    public async Task AddTagCourseAsync(TagCourseDto tagCourseDto)
    {
        var tagCourse = new TagCourse
        {
            CourseId = tagCourseDto.CourseId,
            TagId = tagCourseDto.TagId
        };

        _context.TagCourses.Add(tagCourse);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTagCourseAsync(int courseId, int tagId)
    {
        var tagCourse = await _context.TagCourses
            .FirstOrDefaultAsync(tc => tc.CourseId == courseId && tc.TagId == tagId);
        
        if (tagCourse == null)
            return;

        _context.TagCourses.Remove(tagCourse);
        await _context.SaveChangesAsync();
    }
}
}