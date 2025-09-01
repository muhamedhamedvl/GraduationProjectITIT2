
using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseVM>> GetAllAsync();
        Task<CourseVM?> GetByIdAsync(int id);
        Task<CourseVM?> GetWithSessionsAsync(int id);
        Task<IEnumerable<CourseVM>> SearchByNameAsync(string name);
        Task AddAsync(CourseVM vm);
        Task UpdateAsync(CourseVM vm);
        Task DeleteAsync(int id);

    }
}
