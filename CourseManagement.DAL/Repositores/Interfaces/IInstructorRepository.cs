using CourseManagement.DAL.Entites;

namespace CourseManagement.DAL.Interfaces
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
    }
}
