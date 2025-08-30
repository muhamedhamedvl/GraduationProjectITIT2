
﻿using CourseManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetPagedCoursesAsync(string? search, string? category, int page, int pageSize);
        Task<int> GetCoursesCountAsync(string? search, string? category);
        Task<Course?> GetCourseAsync(int id);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<bool> IsCourseNameUniqueAsync(string name, int? excludeId = null);
    

    }
}
