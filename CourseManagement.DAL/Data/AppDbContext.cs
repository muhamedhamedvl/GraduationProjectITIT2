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
                public DbSet<Instructor> Instructors { get; set; }

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
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Session)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SessionId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Trainee)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.TraineeId)
                .OnDelete(DeleteBehavior.SetNull);

            //Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Ahmed Reda", Email = "ahmed@example.com", Role = Role.Instructor }
            );
        }
    }
}
