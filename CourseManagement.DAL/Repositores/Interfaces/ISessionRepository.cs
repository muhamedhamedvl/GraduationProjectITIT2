using CourseManagement.DAL.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositores.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetAllSessionsAsync(string courseName = null, int page = 1, int pageSize = 10);

        Task<Session> GetByIdWithCourseAsync(int id);

        Task<IEnumerable<Session>> GetSessionsByCourseIdAsync(int courseId);

        Task AddSessionAsync(Session session);

        Task UpdateSessionAsync(Session session);

        Task DeleteSessionAsync(int id);
    }
}
