using LearningPlatform.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningPlatform.Services;

namespace LearningPlatform.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        var tags = await _tagService.GetAllTagsAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTagById(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);
        if (tag == null)
            return NotFound();

        return Ok(tag);
    }

    [HttpPost]
    public async Task<IActionResult> AddTag([FromBody] TagDto tagDto)
    {
        await _tagService.AddTagAsync(tagDto);
        return CreatedAtAction(nameof(GetTagById), new { id = tagDto.Id }, tagDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTag([FromBody] TagDto tagDto)
    {
        await _tagService.UpdateTagAsync(tagDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        await _tagService.DeleteTagAsync(id);
        return NoContent();
    }
}
}