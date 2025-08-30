using CourseManagement.DAL.Data;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositories
{
    public class CourseRepository:ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync(string? search, string? category, int page, int pageSize)
        {
            var query = _context.Courses.Include(c => c.Instructor).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(c => c.Category.Contains(category));

            return await query
     .OrderByDescending(c => c.Id)
     .Skip((page - 1) * pageSize)
     .Take(pageSize)
     .ToListAsync();

        }

        //public async Task<int> CountAsync(string? search, string? category)
        //{
        //    var query = _context.Courses.AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(search))
        //        query = query.Where(c => c.Name.Contains(search));

        //    if (!string.IsNullOrWhiteSpace(category))
        //        query = query.Where(c => c.Category.Contains(category));

        //    return await query.CountAsync();
        //}
        public async Task<int> CountAsync(string? search, string? category)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(c => c.Category.Contains(category));

            return await query.CountAsync();
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsNameUniqueAsync(string name, int? excludeId = null)
        {
            return !await _context.Courses
                .AnyAsync(c => c.Name == name && (!excludeId.HasValue || c.Id != excludeId.Value));
        }
    }

}


       
