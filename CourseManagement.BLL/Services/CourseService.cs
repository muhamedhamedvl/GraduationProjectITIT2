using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Course>> GetPagedCoursesAsync(string? search, string? category, int page, int pageSize)
            => _repo.GetAllAsync(search, category, page, pageSize);

        public Task<int> GetCoursesCountAsync(string? search, string? category)
            => _repo.CountAsync(search, category);
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _repo.GetAllCoursesAsync();
        }
        public Task<Course?> GetCourseAsync(int id)
            => _repo.GetByIdAsync(id);

        public Task AddCourseAsync(Course course)
            => _repo.AddAsync(course);

        public Task UpdateCourseAsync(Course course)
            => _repo.UpdateAsync(course);

        public Task DeleteCourseAsync(int id)
            => _repo.DeleteAsync(id);

        public Task<bool> IsCourseNameUniqueAsync(string name, int? excludeId = null)
            => _repo.IsNameUniqueAsync(name, excludeId);
    }

}
