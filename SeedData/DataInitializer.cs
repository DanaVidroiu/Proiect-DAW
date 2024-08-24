using LearningPlatform.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

public class DataInitializer
{
    public static async Task Initialize(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        if (context.Courses.Any())
        {
            // Baza de date deja populată
            return;
        }

        await CleanUpIncompleteCourses(context); 
        await SeedRoles(roleManager);
        await SeedUsers(userManager, roleManager);
        await SeedCourses(context);
        await SeedLessons(context);
        await SeedEnrollments(context);
        await SeedCourseStatistics(context);
    }

    private static async Task CleanUpIncompleteCourses(ApplicationDbContext context)
    {
        var incompleteCourses = context.Courses
            .Where(c => string.IsNullOrEmpty(c.Title) 
                || string.IsNullOrEmpty(c.Description) 
                || string.IsNullOrEmpty(c.Level) 
                || string.IsNullOrEmpty(c.Category) 
                || c.Price < 0 // Verificare pentru preț negativ
                || c.ProfessorId == null); // Verificare dacă ProfessorId este null

        context.Courses.RemoveRange(incompleteCourses);
        await context.SaveChangesAsync();
    }

    public static async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
    {
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
            }
        }
    }

    private static async Task SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        var users = new User[]
        {
            new User { Name = "John Doe", Email = "JohnDoe@gmail.com", IsProfessor = true, UserName = "JohnDoe1" },
            new User { Name = "Jane Nelson", Email = "JaneNelson@gmail.com", IsProfessor = true, UserName = "Jane44"  },
            new User { Name = "Benjamin Smith", Email = "BenjaminSmith@gmail.com", IsProfessor = true, UserName = "BenjaminF3" },
            new User { Name = "Antonio Johnson", Email = "AntonioJohnson@gmail.com", IsProfessor = false, UserName = "Antonio90" },
            new User { Name = "Louis David", Email = "LouisDavid@gmail.com", IsProfessor = false, UserName = "LouisDavid33" },
            new User { Name = "Eduard Mitchell", Email = "EduardMitchell@gmail.com", IsProfessor = false, UserName = "Eduardx9" },
            new User { Name = "Jane Smith", Email = "JaneSmith@gmail.com", IsProfessor = false, UserName = "JaneSmithh" },
            new User { Name = "Charlie Brown", Email = "CharlieBrown@gmail.com", IsProfessor = false, UserName = "Charlie831" },
            new User { Name = "Jessica White", Email = "JessicaWhite@gmail.com", IsProfessor = false, UserName = "JessicaWhite1" },
            new User { Name = "Elisabeta Davis", Email = "ElisabetaDavis@gmail.com", IsProfessor = false, UserName = "Elisabetaa" },
            new User { Name = "Thomas Norman", Email = "ThomasNorman@gmail.com", IsProfessor = false, UserName = "Thomas21" },
        };

        foreach (var user in users)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email))
            {
                throw new Exception("UserName and Email must be provided and cannot be empty.");
            }
            if (userManager.Users.All(u => u.Email != user.Email))
            {
                user.NormalizedUserName = user.UserName.ToUpper();
                user.NormalizedEmail = user.Email.ToUpper();

                var result = await userManager.CreateAsync(user, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");

                    if (user.IsProfessor)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
                else
                {
                    throw new Exception($"Failed to create user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }   

    private static async Task SeedCourses(ApplicationDbContext context)
    {
        if (!context.Courses.Any())
        {
            var courses = new Course[]
            {
                new Course
                {
                    Title = "Introduction to C#",
                    Description = "Learn the basics of C# programming language, including syntax, data types, and control structures.",
                    Level = "Beginner",
                    Category = "Programming",
                    Price = 100,
                    IsPublished = true,
                    ProfessorId = 1,
                    Duration = "10 weeks"
                },
                new Course
                {
                    Title = "Advanced SQL",
                    Description = "Dive deep into SQL with advanced topics such as indexing, performance tuning, and complex queries.",
                    Level = "Advanced",
                    Category = "Database",
                    Price = 120,
                    IsPublished = false,
                    ProfessorId = 2,
                    Duration = "13 weeks"
                },
                new Course
                {
                    Title = "Web Development with ASP.NET Core",
                    Description = "Build modern web applications using ASP.NET Core, covering MVC, Razor Pages, and Web API.",
                    Level = "Intermediate",
                    Category = "Web Development",
                    Price = 199,
                    IsPublished = true,
                    ProfessorId = 3,
                    Duration = "14 weeks"
                },
                new Course
                {
                    Title = "Introduction to Data Science",
                    Description = "Get started with data science, including data manipulation, visualization, and introductory machine learning.",
                    Level = "Beginner",
                    Category = "Data Science",
                    Price = 159,
                    IsPublished = true,
                    ProfessorId = 2,
                    Duration = "7 weeks"
                }
            };

            foreach (var course in courses)
            {
                if (string.IsNullOrEmpty(course.Title) ||
                string.IsNullOrEmpty(course.Description) ||
                string.IsNullOrEmpty(course.Level) ||
                string.IsNullOrEmpty(course.Category) ||
                course.Price < 0 ||
                course.ProfessorId == null)
                {
                    continue;
                }

                context.Courses.Add(course);
            }

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedLessons(ApplicationDbContext context)
    {
        var lessons = new Lesson[]
        {
            new Lesson { Title = "Introduction to C# - Lesson 1", Content = "Introduction to C# content.", CourseId = 1, DisplayOrder = 1 },
            new Lesson { Title = "Introduction to C# - Lesson 2", Content = "Introduction to C# content.", CourseId = 1, DisplayOrder = 2 },
            new Lesson { Title = "Introduction to C# - Lesson 3", Content = "Introduction to C# content.", CourseId = 1, DisplayOrder = 3 },
            new Lesson { Title = "Advanced SQL - Lesson 1", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 1 },
            new Lesson { Title = "Advanced SQL - Lesson 2", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 2 },
            new Lesson { Title = "Advanced SQL - Lesson 3", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 3 },
            new Lesson { Title = "Advanced SQL - Lesson 4", Content = "Advanced SQL content.", CourseId = 2, DisplayOrder = 4 },
            new Lesson { Title = "Intermediate Web Development - Lesson 1", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 1 },
            new Lesson { Title = "Intermediate Web Development - Lesson 2", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 2 },        
            new Lesson { Title = "Intermediate Web Development - Lesson 3", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 3 },        
            new Lesson { Title = "Intermediate Web Development - Lesson 4", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 4 },        
            new Lesson { Title = "Intermediate Web Development - Lesson 5", Content = "Intermediate Web Development content.", CourseId = 3, DisplayOrder = 5 },
            new Lesson { Title = "Introduction to Data Science - Lesson 1", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 1 },
            new Lesson { Title = "Introduction to Data Science - Lesson 2", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 2 },
            new Lesson { Title = "Introduction to Data Science - Lesson 3", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 3 },
            new Lesson { Title = "Introduction to Data Science - Lesson 4", Content = "Introduction to Data Science content.", CourseId = 4, DisplayOrder = 4 }
        };

        context.Lessons.AddRange(lessons);
        await context.SaveChangesAsync();
    }

    private static async Task SeedEnrollments(ApplicationDbContext context)
    {
        var enrollments = new Enrollment[]
        {
            new Enrollment { UserId = 4, CourseId = 1, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 5, CourseId = 1, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 10, CourseId = 1, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 9, CourseId = 2, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 11, CourseId = 2, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 5, CourseId = 2, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 8, CourseId = 2, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 7, CourseId = 2, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 6, CourseId = 3, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 7, CourseId = 3, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 11, CourseId = 3, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 4, CourseId = 3, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 10, CourseId = 4, EnrollmentDate = DateTime.UtcNow },
            new Enrollment { UserId = 4, CourseId = 4, EnrollmentDate = DateTime.UtcNow }
        };

        context.Enrollments.AddRange(enrollments);
        await context.SaveChangesAsync();
    }

    private static async Task SeedCourseStatistics(ApplicationDbContext context)
    {
        var courseStatistics = new CourseStatistics[]
        {
            new CourseStatistics { CourseId = 1, EnrollmentsCount = 3, AverageRating = 4.5, TotalRevenue = 300 },
            new CourseStatistics { CourseId = 2, EnrollmentsCount = 5, AverageRating = 0.0, TotalRevenue = 600 },
            new CourseStatistics { CourseId = 3, EnrollmentsCount = 4, AverageRating = 4.8, TotalRevenue = 796 },
            new CourseStatistics { CourseId = 4, EnrollmentsCount = 2, AverageRating = 4.7, TotalRevenue = 318 }
        };

        // Verificare dacă statistica se referă la cursuri existente
        foreach (var stats in courseStatistics)
        {
            if (!context.Courses.Any(c => c.Id == stats.CourseId))
            {
                throw new Exception($"Course with ID {stats.CourseId} does not exist.");
            }
        }

        context.CourseStatistics.AddRange(courseStatistics);
        await context.SaveChangesAsync();
    }
}
