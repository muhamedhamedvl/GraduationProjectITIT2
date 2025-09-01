using CourseManagement.DAL.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositores.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetByCourseIdAsync(int courseId);
    }
}
