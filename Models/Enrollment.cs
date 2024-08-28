using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; 

namespace LearningPlatform.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = new User();

        [Required]
        public int CourseId { get; set; }

        [JsonIgnore]
        public Course Course { get; set; } = new Course();

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}
