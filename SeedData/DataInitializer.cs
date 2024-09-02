using LearningPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class DataInitializer
{
    private static readonly int defaultUserId = -1; 
    private static readonly int defaultCourseId = -1;

    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var services = serviceScope.ServiceProvider;

        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var context = services.GetRequiredService<ApplicationDbContext>();
    
        await SeedRolesAsync(roleManager);

        await SeedUsersAsync(userManager, roleManager);

        await SeedTagAsync(context);
    
        await SeedCoursesAsync(context, userManager);

        await SeedLessonsAsync(context);

        await SeedEnrollmentsAsync(context, userManager);

        await SeedCourseStatisticsAsync(context);
    }


    public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            var roles = new[] { "Professor", "Student" };

           foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
                }
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {

        if(!userManager.Users.Any())
        {
            var users = new List<User>
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
                new User { Name = "Theodor Ken", Email = "TheodorKen@gmail.com", IsProfessor = false, UserName = "TheoTheo" },
                new User { Name = "Mary Isabell", Email = "MaryIsabell@gmail.com", IsProfessor = false, UserName = "Mary10Isa" },    
            };

            foreach (var user in users)
            {
                var password = "DefaultPassword@123";

                var userExists = await userManager.FindByEmailAsync(user.Email);
                if(userExists == null)
                {
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        var createdUser = await userManager.FindByEmailAsync(user.Email);
                        if (createdUser?.Id == null)
                        {
                            Console.WriteLine($"User {user.Email} does not have a valid Id after creation.");
                            continue;
                        }

                        if (user.IsProfessor)
                        {
                            await userManager.AddToRoleAsync(user, "Professor");
                            Console.WriteLine($"Added {user.UserName} to Professor role.");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, "Student");
                            Console.WriteLine($"Added {user.UserName} to Student role.");
                        }
                    } 
                    else 
                    {
                        Console.WriteLine($"Failed to create user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }  
                }
            }
        }
    }

    private static async Task SeedTagAsync(ApplicationDbContext context)
    {
        if (!context.Tags.Any())
        {
            context.Tags.AddRange(new List<Tag>
            {
                new Tag { Name = "Programming" },
                new Tag { Name = "Design" },
                new Tag { Name = "Mathematics" }
            });
            await context.SaveChangesAsync();
        }
    }


    private static async Task SeedCoursesAsync(ApplicationDbContext context, UserManager<User> userManager)
    {
        if (!context.Courses.Any())
        {
            var professors = await userManager.GetUsersInRoleAsync("Professor");

            if (professors == null || professors.Count < 3)
            {
                throw new InvalidOperationException("Nu s-au găsit profesori în baza de date.");
            }

            var professor1 = professors.ElementAtOrDefault(0);
            var professor2 = professors.ElementAtOrDefault(1);
            var professor3 = professors.ElementAtOrDefault(2);

            context.Courses.AddRange(new List<Course>
            {
                 new Course
                {
                    CourseId = 1,
                    Title = "Introduction to C#",
                    Description = "Learn the basics of C# programming language, including syntax, data types, and control structures.",
                    Level = "Beginner",
                    Category = "Programming",
                    Price = 100,
                    IsPublished = true,
                    ProfessorId = professor1?.Id ?? throw new InvalidOperationException("Nu există suficienți profesori disponibili."),
                    Duration = "10 weeks"
                },
                new Course
                {
                    CourseId = 2,
                    Title = "Advanced SQL",
                    Description = "Dive deep into SQL with advanced topics such as indexing, performance tuning, and complex queries.",
                    Level = "Advanced",
                    Category = "Database",
                    Price = 120,
                    IsPublished = false,
                    ProfessorId = professor2?.Id ?? throw new InvalidOperationException("Nu există suficienți profesori disponibili."),
                    Duration = "13 weeks"
                },
                new Course
                {
                    CourseId = 3,
                    Title = "Web Development with ASP.NET Core",
                    Description = "Build modern web applications using ASP.NET Core, covering MVC, Razor Pages, and Web API.",
                    Level = "Intermediate",
                    Category = "Web Development",
                    Price = 199,
                    IsPublished = true,
                    ProfessorId = professor3?.Id ?? throw new InvalidOperationException("Nu există suficienți profesori disponibili."),
                    Duration = "14 weeks"
                },
                new Course
                {
                    CourseId = 4,
                    Title = "Introduction to Data Science",
                    Description = "Get started with data science, including data manipulation, visualization, and introductory machine learning.",
                    Level = "Beginner",
                    Category = "Data Science",
                    Price = 159,
                    IsPublished = true,
                    ProfessorId = professor2?.Id ?? throw new InvalidOperationException("Nu există suficienți profesori disponibili."),
                    Duration = "7 weeks"
                }
            });
            await context.SaveChangesAsync();
        }
    }



    private static async Task SeedLessonsAsync(ApplicationDbContext context)
    {
        var courses = await context.Courses.ToListAsync();
        var course = context.Courses.FirstOrDefault(c => c.Title == "Introduction to C#");

        if (course != null && !context.Lessons.Any())
        {
            context.Lessons.AddRange(new List<Lesson>
            {
                new Lesson { LessonId = 10, Title = "Introduction to C# - Lesson 1", Content = "Introduction to C# content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to C#")?.CourseId ?? 0, DisplayOrder = 1 },
                new Lesson { LessonId = 11, Title = "Introduction to C# - Lesson 2", Content = "Introduction to C# content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to C#")?.CourseId ?? 0, DisplayOrder = 2 },
                new Lesson { LessonId = 12, Title = "Introduction to C# - Lesson 3", Content = "Introduction to C# content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to C#")?.CourseId ?? 0, DisplayOrder = 3 },
                new Lesson { LessonId = 20, Title = "Advanced SQL - Lesson 1", Content = "Advanced SQL content.", CourseId = courses.FirstOrDefault(c => c.Title == "Advanced SQL")?.CourseId ?? 0, DisplayOrder = 1 },
                new Lesson { LessonId = 21, Title = "Advanced SQL - Lesson 2", Content = "Advanced SQL content.", CourseId = courses.FirstOrDefault(c => c.Title == "Advanced SQL")?.CourseId ?? 0, DisplayOrder = 2 },
                new Lesson { LessonId = 22, Title = "Advanced SQL - Lesson 3", Content = "Advanced SQL content.", CourseId = courses.FirstOrDefault(c => c.Title == "Advanced SQL")?.CourseId ?? 0, DisplayOrder = 3 },
                new Lesson { LessonId = 23, Title = "Advanced SQL - Lesson 4", Content = "Advanced SQL content.", CourseId = courses.FirstOrDefault(c => c.Title == "Advanced SQL")?.CourseId ?? 0, DisplayOrder = 4 },
                new Lesson { LessonId = 30, Title = "Intermediate Web Development - Lesson 1", Content = "Intermediate Web Development content.", CourseId = courses.FirstOrDefault(c => c.Title == "Web Development with ASP.NET Core")?.CourseId ?? 0, DisplayOrder = 1 },
                new Lesson { LessonId = 31, Title = "Intermediate Web Development - Lesson 2", Content = "Intermediate Web Development content.", CourseId = courses.FirstOrDefault(c => c.Title == "Web Development with ASP.NET Core")?.CourseId ?? 0, DisplayOrder = 2 },        
                new Lesson { LessonId = 32, Title = "Intermediate Web Development - Lesson 3", Content = "Intermediate Web Development content.", CourseId = courses.FirstOrDefault(c => c.Title == "Web Development with ASP.NET Core")?.CourseId ?? 0, DisplayOrder = 3 },        
                new Lesson { LessonId = 33, Title = "Intermediate Web Development - Lesson 4", Content = "Intermediate Web Development content.", CourseId = courses.FirstOrDefault(c => c.Title == "Web Development with ASP.NET Core")?.CourseId ?? 0, DisplayOrder = 4 },        
                new Lesson { LessonId = 34, Title = "Intermediate Web Development - Lesson 5", Content = "Intermediate Web Development content.", CourseId = courses.FirstOrDefault(c => c.Title == "Web Development with ASP.NET Core")?.CourseId ?? 0, DisplayOrder = 5 },
                new Lesson { LessonId = 40, Title = "Introduction to Data Science - Lesson 1", Content = "Introduction to Data Science content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to Data Science")?.CourseId ?? 0, DisplayOrder = 1 },
                new Lesson { LessonId = 41, Title = "Introduction to Data Science - Lesson 2", Content = "Introduction to Data Science content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to Data Science")?.CourseId ?? 0, DisplayOrder = 2 },
                new Lesson { LessonId = 42, Title = "Introduction to Data Science - Lesson 3", Content = "Introduction to Data Science content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to Data Science")?.CourseId ?? 0, DisplayOrder = 3 },
                new Lesson { LessonId = 43, Title = "Introduction to Data Science - Lesson 4", Content = "Introduction to Data Science content.", CourseId = courses.FirstOrDefault(c => c.Title == "Introduction to Data Science")?.CourseId ?? 0, DisplayOrder = 4 }
            });
            await context.SaveChangesAsync();
        }
    }


private static async Task SeedEnrollmentsAsync(ApplicationDbContext context, UserManager<User> userManager)
{
    if (!context.Enrollments.Any())
    {
        var students = await userManager.Users
            .Where(u => !u.IsProfessor)
            .ToListAsync();

        // Verificăm dacă Email-ul nu este null sau gol înainte de a-l adăuga în dicționar
        var studentDict = students
            .Where(s => !string.IsNullOrEmpty(s.Email))  // Exclude studenții fără email
            .ToDictionary(s => s.Email, s => s.Id);

        var courses = await context.Courses.ToListAsync();

        // Verificăm dacă Title-ul nu este null sau gol înainte de a-l adăuga în dicționar
        var courseDict = courses
            .Where(c => !string.IsNullOrEmpty(c.Title))  // Exclude cursurile fără titlu
            .ToDictionary(c => c.Title, c => c.CourseId);


        Console.WriteLine("Students:");
        foreach (var student in studentDict)
        {
            Console.WriteLine($"Email: {student.Key}, ID: {student.Value}");
        }

        Console.WriteLine("Courses:");
        foreach (var course in courseDict)
        {
            Console.WriteLine($"Title: {course.Key}, ID: {course.Value}");
        }

        var enrollments = new List<Enrollment>
        {
            new Enrollment { UserId = studentDict.GetValueOrDefault("AntonioJohnson@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Introduction to C#", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-24) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("LouisDavid@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Introduction to C#", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-26) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("JaneSmith@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Introduction to C#", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-29) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("CharlieBrown@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Advanced SQL", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-21) }, 
            new Enrollment { UserId = studentDict.GetValueOrDefault("ElisabetaDavis@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Advanced SQL", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-19) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("EduardMitchell@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Advanced SQL", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-15) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("JessicaWhite@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Advanced SQL", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-15) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("ThomasNorman@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Web Development with ASP.NET Core", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-18) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("MaryIsabell@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Web Development with ASP.NET Core", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-15) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("CharlieBrown@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Web Development with ASP.NET Core", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-14) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("ElisabetaDavis@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Web Development with ASP.NET Core", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-13) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("MaryIsabell@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Introduction to Data Science", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-10) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("LouisDavid@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Introduction to Data Science", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-9) },
            new Enrollment { UserId = studentDict.GetValueOrDefault("EduardMitchell@gmail.com", defaultUserId), CourseId = courseDict.GetValueOrDefault("Introduction to Data Science", defaultCourseId), EnrollmentDate = DateTime.UtcNow.AddDays(-5) }
        };

        context.Enrollments.AddRange(enrollments);
        await context.SaveChangesAsync();
    }
}


    private static async Task SeedCourseStatisticsAsync(ApplicationDbContext context)
    {
        if (!context.CourseStatistics.Any())
        {
            var courses = await context.Courses
                .GroupBy(c => c.Title)
                .ToDictionaryAsync(g => g.Key, global => global.First().CourseId);

            var courseStatistics = new List<CourseStatistics>
            {
                new CourseStatistics 
                { 
                    CourseId = courses.GetValueOrDefault("Introduction to C#"),
                    TotalRevenue = 300.00m, 
                    TotalEnrollments = 3
                },
                new CourseStatistics 
                { 
                    CourseId = courses.GetValueOrDefault("Advanced SQL"),
                    TotalRevenue = 480.00m, 
                    TotalEnrollments = 4
                },
                new CourseStatistics 
                { 
                    CourseId = courses.GetValueOrDefault("Web Development with ASP.NET Core"),
                    TotalRevenue = 796.00m, 
                    TotalEnrollments = 4
                },
                new CourseStatistics 
                { 
                    CourseId = courses.GetValueOrDefault("Introduction to Data Science"),
                    TotalRevenue = 477.00m, 
                    TotalEnrollments = 3
                }
            };
            
        // Filtrăm statisticile cu CourseId invalid (adică 0)
        courseStatistics = courseStatistics.Where(cs => cs.CourseId != 0).ToList();

        if (courseStatistics.Any())
        {
            context.CourseStatistics.AddRange(courseStatistics);
            await context.SaveChangesAsync();
        }
        
        context.CourseStatistics.AddRange(courseStatistics);
        await context.SaveChangesAsync();
        }
    }
}