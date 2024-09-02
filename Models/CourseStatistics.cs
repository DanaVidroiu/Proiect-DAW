using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class CourseStatistics
    {
        public int CourseStatisticsId { get; set; }

        [Required(ErrorMessage = "CourseId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be a positive integer.")]
        public int CourseId { get; set; }
        public Course? Course { get; set; } = new Course();

        [Range(0, int.MaxValue, ErrorMessage = "Enrollments count must be a positive integer.")]
        public int TotalEnrollments { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Total revenue must be a positive number.")]
        public decimal TotalRevenue { get; set; } = 0;
    }
}
