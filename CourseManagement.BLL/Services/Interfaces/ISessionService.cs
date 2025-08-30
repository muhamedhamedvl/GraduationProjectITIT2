using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllSessionsAsync(string courseName = null, int page = 1, int pageSize = 10);
        Task<Session> GetSessionByIdAsync(int id);
        Task AddSessionAsync(SessionViewModel model);
        Task UpdateSessionAsync(int id, SessionViewModel model);
        Task DeleteSessionAsync(int id);
    }
}
