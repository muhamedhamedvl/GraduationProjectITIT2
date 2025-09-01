using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositores
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Grade>> GetGradesByTraineeIdAsync(int traineeId)
        {
            return await _context.Grades
                .Include(g => g.Session)
                    .ThenInclude(s => s.Course)  
                .Where(g => g.TraineeId == traineeId)
                .ToListAsync();
        }
    }
}
