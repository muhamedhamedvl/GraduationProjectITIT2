using CourseManagement.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CourseManagement.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Grade> Grades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Session>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Session)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SessionId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Trainee)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.TraineeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Muhamed Hamed", Email = "admin@test.com", Role = Role.Admin },
                new User { Id = 2, Name = "Ahmed Reda", Email = "AhmedReda@gmail.com", Role = Role.Instructor },
                new User { Id = 3, Name = "Mahmoud", Email = "Mahmoud@test.com", Role = Role.Trainee },
                new User { Id = 4, Name = "Ali", Email = "Ali@test.com", Role = Role.Trainee }
            );

            modelBuilder.Entity<Course>().HasData(
        new Course
        {
            Id = 1,
            Name = "ASP.NET Core MVC",
            Category = Category.Programming,
            Description = "Learn ASP.NET Core MVC from scratch",
            StartDate = new DateTime(2025, 9, 1),
            EndDate = new DateTime(2025, 9, 30),
            InstructorId = 2
        }
        );
            modelBuilder.Entity<Session>().HasData(
                new Session { Id = 1, CourseId = 1, StartDate = new DateTime(2025, 9, 1), EndDate = new DateTime(2025, 9, 5) },
                new Session { Id = 2, CourseId = 1, StartDate = new DateTime(2025, 9, 6), EndDate = new DateTime(2025, 9, 10) }
            );

            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = 1, Value = 90, SessionId = 1, TraineeId = 3 },
                new Grade { Id = 2, Value = 75, SessionId = 1, TraineeId = 4 },
                new Grade { Id = 3, Value = 85, SessionId = 2, TraineeId = 3 }
            );
        }
    }
}
