using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Course> Courses { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbSet<CourseStatistics> CourseStatistics { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCourse> TagCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-To-Many : User <=> Enrollments
            modelBuilder.Entity<User>()
                .HasMany(u => u.Enrollments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            //One-To-Many : User <=> Course 
            modelBuilder.Entity<User>()
                .HasMany(u => u.CoursesCreated)
                .WithOne(c => c.Professor)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //One-To-Many : Course <=> Lesson
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
            
            //One-To-Many : Enrollment <=> Course
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            //One-To-Many : Enrollment <=> User 
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-To-One : Course <=> CourseStatistics
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Statistics) 
                .WithOne(cs => cs.Course)
                .HasForeignKey<CourseStatistics>(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            //Many-To-Many : Course <=> Tag (prin intermediul TagCourse) 
            modelBuilder.Entity<TagCourse>()
                .HasKey(tc => new { tc.CourseId, tc.TagId }); // Setarea cheii compuse

            modelBuilder.Entity<TagCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TagCourses)
                .HasForeignKey(tc => tc.CourseId);

            modelBuilder.Entity<TagCourse>()
                .HasOne(tc => tc.Tag)
                .WithMany(t => t.TagCourses)
                .HasForeignKey(tc => tc.TagId);


            //Unicitate email 
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Unicitate UserName
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();


            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.CourseId, l.Title })
                .IsUnique();

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CourseStatistics>()
                .Property(cs => cs.TotalRevenue)
                .HasColumnType("decimal(18,2)");
        }
    }
}
