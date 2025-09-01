using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositores
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Session>> GetByCourseIdAsync(int courseId)
        {
            return await _context.Sessions.Where(s => s.CourseId == courseId).ToListAsync();
        }
    }
}

