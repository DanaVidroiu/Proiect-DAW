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
        public DbSet<CourseStatistics> CourseStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurări pentru relații și indici
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(u => u.CoursesCreated)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Cascade); 

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
