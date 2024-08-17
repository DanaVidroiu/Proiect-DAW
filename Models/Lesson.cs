using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        [Range(1, int.MaxValue, ErrorMessage = "Display order must be a positive integer.")]
        public int DisplayOrder { get; set; }
    }
}
