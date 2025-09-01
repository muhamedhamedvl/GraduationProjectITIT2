using CourseManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositores.Interfaces
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetGradesByTraineeIdAsync(int traineeId);
    }

}
