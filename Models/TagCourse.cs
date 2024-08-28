using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class TagCourse
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        public int TagId { get; set; }
        public Tag Tag { get; set; } = new Tag();
    }
}
