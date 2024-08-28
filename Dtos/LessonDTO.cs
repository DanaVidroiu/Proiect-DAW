using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Dtos
{
    public class LessonDTO
    {   
        public int LessonId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int CourseId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Display order must be a positive integer.")]
        public int DisplayOrder { get; set; }
    }
}
