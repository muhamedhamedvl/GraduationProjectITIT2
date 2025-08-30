using CourseManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync(string? search, string? category, int page, int pageSize);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<int> CountAsync(string? search, string? category);
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        Task<bool> IsNameUniqueAsync(string name, int? excludeId = null);
    }
}
