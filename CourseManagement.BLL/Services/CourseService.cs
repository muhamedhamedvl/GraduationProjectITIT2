using CourseManagement.BLL.Interfaces;
using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Repositores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;

        public CourseService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<CourseVM>> GetAllAsync()
        {
            var courses = await _courseRepo.GetAllAsync();

            return courses.Select(c => new CourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Category = c.Category,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                InstructorId = (int)c.InstructorId
            });
        }

        public async Task<CourseVM?> GetByIdAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return null;

            return new CourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                InstructorId = (int)course.InstructorId
            };
        }

        public async Task<CourseVM?> GetWithSessionsAsync(int id)
        {
            var course = await _courseRepo.GetCourseWithSessionsAsync(id);
            if (course == null) return null;

            return new CourseVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                InstructorId = (int)course.InstructorId
            };
        }

        public async Task<IEnumerable<CourseVM>> SearchByNameAsync(string name)
        {
            var courses = await _courseRepo.SearchByNameAsync(name);

            return courses.Select(c => new CourseVM
            {
                Id = c.Id,
                Name = c.Name,
                Category = c.Category,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                InstructorId = (int)c.InstructorId
            });
        }

        public async Task AddAsync(CourseVM vm)
        {
            var course = new Course
            {
                Name = vm.Name,
                Category = vm.Category,
                Description = vm.Description,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                InstructorId = vm.InstructorId
            };

            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveAsync();
        }

        public async Task UpdateAsync(CourseVM vm)
        {
            var course = await _courseRepo.GetByIdAsync(vm.Id);
            if (course == null) return;

            course.Name = vm.Name;
            course.Category = vm.Category;
            course.Description = vm.Description;
            course.StartDate = vm.StartDate;
            course.EndDate = vm.EndDate;
            course.InstructorId = vm.InstructorId;

            _courseRepo.Update(course);
            await _courseRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return;

            _courseRepo.Delete(course);
            await _courseRepo.SaveAsync();
        }
    }
}
