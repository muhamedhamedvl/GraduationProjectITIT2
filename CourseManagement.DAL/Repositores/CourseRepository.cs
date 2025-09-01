using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Repositores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }

        public async Task<Course> GetCourseWithSessionsAsync(int id)
        {
            return await _context.Courses.Include(c => c.Sessions).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> SearchByNameAsync(string name)
        {
            return await _context.Courses.Where(c => c.Name.Contains(name)).ToListAsync();
        }
    }
}


       
