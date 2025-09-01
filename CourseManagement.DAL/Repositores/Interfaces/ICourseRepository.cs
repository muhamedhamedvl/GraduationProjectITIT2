using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Repositores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetCourseWithSessionsAsync(int id);
        Task<IEnumerable<Course>> SearchByNameAsync(string name);
    }
}
