using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        [Url]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Bio cannot be longer than 500 characters.")]
        public string Bio { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Course> CoursesCreated { get; set; } = new List<Course>();

        public int StudentsEnrolled { get; set; } = 0;

        public DateTime LastLogin { get; set; }

        public DateTime AccountCreated { get; set; } = DateTime.UtcNow;

        public bool IsProfessor { get; set; }

        public string GetDisplayName()
        {
            return !string.IsNullOrEmpty(Name) ? Name : UserName ?? string.Empty;
        }

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
