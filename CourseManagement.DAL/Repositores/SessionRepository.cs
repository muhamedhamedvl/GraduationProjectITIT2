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
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAllSessionsAsync(string courseName = null, int page = 1, int pageSize = 10)
        {
            var query = _context.Sessions.Include(s => s.Course).AsQueryable();

            if (!string.IsNullOrEmpty(courseName))
            {
                query = query.Where(s => s.Course.Name.Contains(courseName));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Session> GetByIdWithCourseAsync(int id)
        {
            return await _context.Sessions
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Session>> GetSessionsByCourseIdAsync(int courseId)
        {
            return await _context.Sessions
                .Where(s => s.CourseId == courseId)
                .Include(s => s.Course)
                .ToListAsync();
        }

        public async Task AddSessionAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSessionAsync(Session session)
        {
            var existingSession = await _context.Sessions.FindAsync(session.Id);
            if (existingSession != null)
            {
                existingSession.Name = session.Name;
                existingSession.StartDate = session.StartDate;
                existingSession.EndDate = session.EndDate;
                existingSession.CourseId = session.CourseId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSessionAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }
    }
}

