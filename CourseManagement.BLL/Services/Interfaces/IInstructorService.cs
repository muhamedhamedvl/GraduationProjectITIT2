using CourseManagement.DAL.Entites;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.BLL.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllAsync();

    }
}
