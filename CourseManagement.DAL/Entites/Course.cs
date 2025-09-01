using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CourseManagement.DAL.Entites
{
    public enum Category
    {
        Programming,
        Design,
        Marketing,
        Business,
        DataScience,
        DevOps
    }
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? InstructorId { get; set; }
        public User Instructor { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
