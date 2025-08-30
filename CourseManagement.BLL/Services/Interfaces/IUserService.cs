using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserVM>> GetAllAsync();
        Task<UserVM?> GetByIdAsync(int id);
        Task<UserVM?> GetByEmailAsync(string email);

        Task AddAsync(UserVM vm);
        Task UpdateAsync(UserVM vm);
        Task DeleteAsync(int id);
    }
}
