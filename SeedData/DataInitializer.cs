using LearningPlatform.Models;
using System.Linq;

public class DataInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Courses.Any())
        {
            // Baza de date deja populată
            return;
        }

        // Seed Data pentru Useri
        var users = new User[]
        {
            new User { Id = 1, Name = "John Doe", Email = "JohnDoe@gmail.com", IsProfessor = true },
            new User { Id = 2, Name = "Jane Nelson", Email = "JaneNelson@gmail.com", IsProfessor = true },
            new User { Id = 3, Name = "Benjamin Smith", Email = "BenjaminSmith@gmail.com", IsProfessor = true },
            new User { Id = 4, Name = "Antonio Johnson", Email = "AntonioJohnson@gmail.com", IsProfessor = false },
            new User { Id = 5, Name = "Louis David", Email = "LouisDavid@gmail.com", IsProfessor = false },
            new User { Id = 6, Name = "Eduard Mitchell", Email = "EduardMitchell@gmail.com", IsProfessor = false },
            new User { Id = 7, Name = "Jane Smith", Email = "JaneSmith@gmail.com", IsProfessor = false },
            new User { Id = 8, Name = "Charlie Brown", Email = "CharlieBrown@gmail.com", IsProfessor = false },
            new User { Id = 9, Name = "Jessica White", Email = "JessicaWhite@gmail.com", IsProfessor = false },
            new User { Id = 10, Name = "Elisabeta Davis", Email = "ElisabetaDavis@gmail.com", IsProfessor = false },
            new User { Id = 11, Name = "Thomas Norman", Email = "ThomasNorman@gmail.com", IsProfessor = false },
        };
        context.Users.AddRange(users);
        context.SaveChanges();


        // Seed Data pentru Cursuri
        var courses = new Course[]
        {  
        new Course
            {
            Id = 1,
            Title = "Introduction to C#",
            Description = "Learn the basics of C# programming language, including syntax, data types, and control structures.",
            Level = "Beginner",
            Category = "Programming",
            Price = 100,
            IsPublished = true,
            UserId = "1",
            Duration = "10 weeks"
            },
        new Course
        {
            Id = 2,
            Title = "Advanced SQL",
            Description = "Dive deep into SQL with advanced topics such as indexing, performance tuning, and complex queries.",
            Level = "Advanced",
            Category = "Database",
            Price = 120,
            IsPublished = false,
            UserId = "2",
            Duration = "13 weeks"
        },
        new Course
        {
            Id = 3,
            Title = "Web Development with ASP.NET Core",
            Description = "Build modern web applications using ASP.NET Core, covering MVC, Razor Pages, and Web API.",
            Level = "Intermediate",
            Category = "Web Development",
            Price = 199,
            IsPublished = true,
            UserId = "3",
            Duration = "14 weeks"
        },
        new Course
        {
            Id = 4,
            Title = "Introduction to Data Science",
            Description = "Get started with data science, including data manipulation, visualization, and introductory machine learning.",
            Level = "Beginner",
            Category = "Data Science",
            Price = 159,
            IsPublished = true,
            UserId = "2",
            Duration = "7 weeks"
        }
        
        };
        context.Courses.AddRange(courses);
        context.SaveChanges();



        // Seed Data pentru Lecții
        var lessons = new Lesson[]
        {
            new Lesson { Id = 1, Title = "Introduction to C# - Lesson 1", Content = "Introduction to C# content.", CourseId = 1, DisplayOrder = 1 },
            new Lesson { Id = 2, Title = "Introduction to C# - Lesson 2", Content = "Introduction to C# content.", CourseId = 1, DisplayOrder = 2 },
            new Lesson { Id = 3, Title = "Introduction to C# - Lesson 3", Content = "Introduction to C# content.", CourseId = 1, DisplayOrder = 3 },
            new Lesson { Id = 4, Title = "Advanced SQL - Lesson 1", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 1 },
            new Lesson { Id = 5, Title = "Advanced SQL - Lesson 2", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 2 },
            new Lesson { Id = 6, Title = "Advanced SQL - Lesson 3", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 3 },
            new Lesson { Id = 7, Title = "Advanced SQL - Lesson 4", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 4 },
            new Lesson { Id = 8, Title = "Intermediate Web Development - Lesson 1", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 1 },
            new Lesson { Id = 9, Title = "Intermediate Web Development - Lesson 2", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 2 },        
            new Lesson { Id = 10, Title = "Intermediate Web Development - Lesson 3", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 3 },        
            new Lesson { Id = 11, Title = "Intermediate Web Development - Lesson 4", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 4 },        
            new Lesson { Id = 12, Title = "Intermediate Web Development - Lesson 5", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 5 },
            new Lesson { Id = 13, Title = "Introduction to Data Science - Lesson 1", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 1 },
            new Lesson { Id = 14, Title = "Introduction to Data Science - Lesson 2", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 2 },
            new Lesson { Id = 15, Title = "Introduction to Data Science - Lesson 3", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 3 },
            new Lesson { Id = 16, Title = "Introduction to Data Science - Lesson 4", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 4 }
        };
        context.Lessons.AddRange(lessons);
        context.SaveChanges();



        // Seed Data pentru Înscrieri
        var enrollments = new Enrollment[]
        {
            new Enrollment { Id = 1, UserId = "4", CourseId = 1, EnrollmentDate = DateTime.UtcNow }, // Antonio se înscrie la Introduction to C#
            new Enrollment { Id = 2, UserId = "5", CourseId = 1, EnrollmentDate = DateTime.UtcNow },  // Louis se înscrie la Introduction to C#
            new Enrollment { Id = 3, UserId = "10", CourseId = 1, EnrollmentDate = DateTime.UtcNow },  // Elisabeta se înscrie la Introduction to C#
            new Enrollment { Id = 4, UserId = "9", CourseId = 2, EnrollmentDate = DateTime.UtcNow },  //  Jessica se înscrie la Advanced SQL
            new Enrollment { Id = 5, UserId = "11", CourseId = 2, EnrollmentDate = DateTime.UtcNow },  //  Thomas se înscrie la Advanced SQL
            new Enrollment { Id = 6, UserId = "5", CourseId = 2, EnrollmentDate = DateTime.UtcNow },  //  Louis se înscrie la Advanced SQL
            new Enrollment { Id = 7, UserId = "8", CourseId = 2, EnrollmentDate = DateTime.UtcNow },  //  Charlie se înscrie la Advanced SQL
            new Enrollment { Id = 8, UserId = "7", CourseId = 2, EnrollmentDate = DateTime.UtcNow },  // Jane se înscrie la Advanced SQL
            new Enrollment { Id = 9, UserId = "6", CourseId = 3, EnrollmentDate = DateTime.UtcNow },  // Eduard se înscrie la Web Development with ASP.NET Core
            new Enrollment { Id = 10, UserId = "7", CourseId = 3, EnrollmentDate = DateTime.UtcNow },  //  Jane se înscrie la Web Development with ASP.NET Core
            new Enrollment { Id = 11, UserId = "11", CourseId = 3, EnrollmentDate = DateTime.UtcNow },  // Thomas se înscrie la Web Development with ASP.NET Core
            new Enrollment { Id = 12, UserId = "4", CourseId = 3, EnrollmentDate = DateTime.UtcNow },  //  Antonio se înscrie la Web Development with ASP.NET Core
            new Enrollment { Id = 13, UserId = "10", CourseId = 4, EnrollmentDate = DateTime.UtcNow },  // Elisabeta se înscrie la Introduction to Data Science
            new Enrollment { Id = 14, UserId = "4", CourseId = 4, EnrollmentDate = DateTime.UtcNow }  // Antonio se înscrie la Introduction to Data Science
        
        };
        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();



        // Seed Data pentru Statistici Cursuri
        var courseStatistics = new CourseStatistics[]
        {
            new CourseStatistics { Id = 1, CourseId = 1, EnrollmentsCount = 3, AverageRating = 4.5, TotalRevenue = 300 },
            new CourseStatistics { Id = 2, CourseId = 2, EnrollmentsCount = 5, AverageRating = 0.0, TotalRevenue = 600 },
            new CourseStatistics { Id = 3, CourseId = 3, EnrollmentsCount = 4, AverageRating = 4.8, TotalRevenue = 796 },
            new CourseStatistics { Id = 4, CourseId = 4, EnrollmentsCount = 2, AverageRating = 4.7, TotalRevenue = 318 }
        };

        context.CourseStatistics.AddRange(courseStatistics);
        context.SaveChanges();

    }
}
