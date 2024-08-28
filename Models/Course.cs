using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearningPlatform.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Level { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10,000.")]
        public int Price { get; set; }

        public bool IsPublished { get; set; }

        public int ProfessorId { get; set; }  
    
        [JsonIgnore] 
        public User Professor { get; set; } = new User();

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public ICollection<TagCourse> TagCourses { get; set; } = new List<TagCourse>();

        public string Duration { get; set; } = string.Empty; 

        public CourseStatistics? Statistics { get; set; }
    }
}
