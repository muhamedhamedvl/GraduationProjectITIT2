using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Repositores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepo;
        private readonly ICourseRepository _courseRepo;

        public SessionService(ISessionRepository sessionRepo, ICourseRepository courseRepo)
        {
            _sessionRepo = sessionRepo;
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<Session>> GetAllSessionsAsync(string courseName = null, int page = 1, int pageSize = 10)
        {
            var query = await _sessionRepo.GetAllAsync();

            if (!string.IsNullOrEmpty(courseName))
            {
                query = query.Where(s => s.Course.Name.Contains(courseName, StringComparison.OrdinalIgnoreCase));
            }

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<Session> GetSessionByIdAsync(int id)
        {
            return await _sessionRepo.GetByIdAsync(id);
        }

        public async Task AddSessionAsync(SessionViewModel model)
        {
            if (model.StartDate < DateTime.Today)
                throw new ArgumentException("Start date cannot be in the past.");

            if (model.EndDate <= model.StartDate)
                throw new ArgumentException("End date must be after start date.");

            var course = await _courseRepo.GetByIdAsync(model.CourseId);
            if (course == null)
                throw new ArgumentException("Invalid course selected.");

            var session = new Session
            {
                Name = model.Name,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CourseId = model.CourseId
            };

            await _sessionRepo.AddAsync(session);
        }

        public async Task UpdateSessionAsync(int id, SessionViewModel model)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null)
                throw new ArgumentException("Session not found.");

            if (model.StartDate < DateTime.Today)
                throw new ArgumentException("Start date cannot be in the past.");

            if (model.EndDate <= model.StartDate)
                throw new ArgumentException("End date must be after start date.");

            var course = await _courseRepo.GetByIdAsync(model.CourseId);
            if (course == null)
                throw new ArgumentException("Invalid course selected.");

            session.Name = model.Name;
            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.CourseId = model.CourseId;

            await _sessionRepo.UpdateSessionAsync(session);
        }

        public async Task DeleteSessionAsync(int id)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session != null)
            {
                await _sessionRepo.DeleteSessionAsync(id);
            }
        }
    }
}
