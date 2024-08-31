using LearningPlatform.Models;
using LearningPlatform.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlatform.Services.TagService
{
public class TagService : ITagService
{
    private readonly ApplicationDbContext _context;

    public TagService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        return await _context.Tags
            .Select(tag => new TagDto
            {
                Id = tag.TagId,
                Name = tag.Name
            }).ToListAsync();
    }

    public async Task<TagDto> GetTagByIdAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            return null;

        return new TagDto
        {
            Id = tag.TagId,
            Name = tag.Name
        };
    }

    public async Task AddTagAsync(TagDto tagDto)
    {
        var tag = new Tag
        {
            Name = tagDto.Name
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTagAsync(TagDto tagDto)
    {
        var tag = await _context.Tags.FindAsync(tagDto.Id);
        if (tag == null)
            return;

        tag.Name = tagDto.Name;
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTagAsync(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
            return;

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();
    }
}
}