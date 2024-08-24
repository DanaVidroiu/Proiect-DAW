using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Dtos
{
public class LessonDTO
{

    public int Id { get; set; }
    public LessonDTO()
    {
        Title = string.Empty;
        Content = string.Empty;
    }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    public int CourseId { get; set; }
    public int DisplayOrder { get; set; }
}

}
