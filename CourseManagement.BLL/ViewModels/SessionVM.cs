using CourseManagement.BLL.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.BLL.ViewModels
{
    public class SessionVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateAfter("StartDate", ErrorMessage = "End Date must be after Start Date")]
        public DateTime EndDate { get; set; }

        [Required]
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
}
