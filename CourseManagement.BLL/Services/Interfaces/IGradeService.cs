using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeVM>> GetAllAsync();
        Task<GradeVM?> GetByIdAsync(int id);
        Task<IEnumerable<GradeVM>> GetByTraineeIdAsync(int traineeId);

        Task AddAsync(GradeVM vm);
        Task UpdateAsync(GradeVM vm);
        Task DeleteAsync(int id);
    }
}
