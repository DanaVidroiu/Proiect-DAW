using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tag name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        public ICollection<TagCourse> TagCourses { get; set; } = new List<TagCourse>();
    }
}
