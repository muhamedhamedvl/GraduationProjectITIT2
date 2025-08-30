using CourseManagement.DAL.Models;

namespace CourseManagement.DAL.Interfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
    }
}
