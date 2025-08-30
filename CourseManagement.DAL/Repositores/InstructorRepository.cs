using CourseManagement.DAL.Data;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DAL.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _ctx;

        public InstructorRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _ctx.Instructors.AsNoTracking().ToListAsync();
        }
    }
}
