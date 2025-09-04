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

        public async Task<IEnumerable<SessionVM>> GetAllAsync()
        {
            var sessions = await _sessionRepo.GetAllAsync();
            var result = new List<SessionVM>();

            foreach (var s in sessions)
            {
                var course = await _courseRepo.GetByIdAsync(s.CourseId);

                result.Add(new SessionVM
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    CourseId = s.CourseId,
                    CourseName = course?.Name 
                });
            }

            return result;
        }

        public async Task<SessionVM?> GetByIdAsync(int id)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null) return null;

            var course = await _courseRepo.GetByIdAsync(session.CourseId);

            return new SessionVM
            {
                Id = session.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseId = session.CourseId,
                CourseName = course?.Name 
            };
        }

        public async Task<IEnumerable<SessionVM>> GetByCourseIdAsync(int courseId)
        {
            var sessions = await _sessionRepo.GetByCourseIdAsync(courseId);
            var course = await _courseRepo.GetByIdAsync(courseId);

            return sessions.Select(s => new SessionVM
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                CourseId = s.CourseId,
                CourseName = course?.Name 
            });
        }

        public async Task AddAsync(SessionVM vm)
        {
            var session = new Session
            {

                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                CourseId = vm.CourseId
            };

            await _sessionRepo.AddAsync(session);
            await _sessionRepo.SaveAsync();
        }

        public async Task UpdateAsync(SessionVM vm)
        {
            var session = await _sessionRepo.GetByIdAsync(vm.Id);
            if (session == null) return;

            session.StartDate = vm.StartDate;
            session.EndDate = vm.EndDate;
            session.CourseId = vm.CourseId;

            _sessionRepo.Update(session);
            await _sessionRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _sessionRepo.GetByIdAsync(id);
            if (session == null) return;

            _sessionRepo.Delete(session);
            await _sessionRepo.SaveAsync();
        }
    }
}