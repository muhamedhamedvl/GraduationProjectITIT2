using System.ComponentModel.DataAnnotations;

namespace CourseManagement.BLL.ViewModels
{
    public class GradeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Grade value is required")]
        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
        public int Value { get; set; }

        [Required(ErrorMessage = "Session is required")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Trainee is required")]
        public int TraineeId { get; set; }
        public string? CourseName { get; set; }
    }
}
