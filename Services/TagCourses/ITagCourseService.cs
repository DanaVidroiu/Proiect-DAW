using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.Services
{
    public interface ITagCourseService
    {
        Task<IEnumerable<TagCourseDto>> GetAllTagCoursesAsync();
        Task<TagCourseDto> GetTagCourseByIdsAsync(int courseId, int tagId);
        Task AddTagCourseAsync(TagCourseDto tagCourseDto);
        Task DeleteTagCourseAsync(int courseId, int tagId);
    }
}
