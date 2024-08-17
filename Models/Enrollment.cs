using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }
        public User User { get; set; } = new User();

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}
