using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class CourseStatistics
    {
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        [Range(0, int.MaxValue, ErrorMessage = "Enrollments count must be a positive integer.")]
        public int EnrollmentsCount { get; set; }

        [Range(0, 5, ErrorMessage = "Average rating must be between 0 and 5.")]
        public double AverageRating { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Total revenue must be a positive number.")]
        public decimal TotalRevenue { get; set; } = 0;
    }
}
