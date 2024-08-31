using System;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Dtos
{
    public class TagCourseDto
    {
        public int CourseId { get; set; }
        public int TagId { get; set; }
    }

}