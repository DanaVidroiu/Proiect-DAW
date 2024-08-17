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
                .HasIndex(e => new { e.UserId, e.CourseId })
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasOne<User>()
                .WithMany(u => u.CoursesCreated)
                .HasForeignKey(c => c.UserId);

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
