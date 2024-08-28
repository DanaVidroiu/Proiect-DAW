using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Dtos
{
    public class EnrollmentDTO
    {
        public int EnrollmentId { get; set; }

        [Required]
        public int UserId { get; set; }
    
        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
