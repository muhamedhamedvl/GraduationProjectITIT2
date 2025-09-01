using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionVM>> GetAllAsync();
        Task<SessionVM?> GetByIdAsync(int id);
        Task<IEnumerable<SessionVM>> GetByCourseIdAsync(int courseId);

        Task AddAsync(SessionVM vm);
        Task UpdateAsync(SessionVM vm);
        Task DeleteAsync(int id);
    }
}
