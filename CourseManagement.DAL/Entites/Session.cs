using CourseManagement.DAL.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.DAL.Entites
{
    public class Session
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
