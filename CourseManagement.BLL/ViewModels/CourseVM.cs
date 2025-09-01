using CourseManagement.BLL.Validations;
using CourseManagement.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Course name must be between 3 and 50 characters")]
        [Remote(action: "IsCourseNameUnique", controller: "Course", ErrorMessage = "Course name already exists")]
        [NoNumber(ErrorMessage = "Course name cannot contain numbers")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public required Category Category { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [FutureDateAttribute(ErrorMessage = "Start date cannot be in the past")]
        public required DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [DateAfterAttribute("StartDate", ErrorMessage = "End date must be after start date")]
        public required DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Instructor is required")]
        public required int InstructorId { get; set; }
    }
}
