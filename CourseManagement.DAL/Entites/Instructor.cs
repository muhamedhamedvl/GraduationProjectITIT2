using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Entites
{

    public class Instructor
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public string? Bio { get; set; }

        [Required]
        public string Specialization { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }



}
