using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.Services.TagService
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllTagsAsync();
        Task<TagDto> GetTagByIdAsync(int id);
        Task AddTagAsync(TagDto tagDto);
        Task UpdateTagAsync(TagDto tagDto);
        Task DeleteTagAsync(int id);
    }
}    
