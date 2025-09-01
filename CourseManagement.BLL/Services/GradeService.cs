using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Repositores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepo;

        public GradeService(IGradeRepository gradeRepo)
        {
            _gradeRepo = gradeRepo;
        }

        public async Task<IEnumerable<GradeVM>> GetAllAsync()
        {
            var grades = await _gradeRepo.GetAllAsync();

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId,
                TraineeId = (int)g.TraineeId,
                CourseName = g.Session?.Course?.Name ?? "Unknown"
            });
        }

        public async Task<GradeVM> GetByIdAsync(int id)
        {
            var g = await _gradeRepo.GetByIdAsync(id);

            return new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId,
                TraineeId = (int)g.TraineeId,
                CourseName = g.Session?.Course?.Name ?? "Unknown"
            };
        }

        public async Task<IEnumerable<GradeVM>> GetByTraineeIdAsync(int traineeId)
        {
            var grades = await _gradeRepo.GetGradesByTraineeIdAsync(traineeId);

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId,
                TraineeId = (int)g.TraineeId,
                CourseName = g.Session?.Course?.Name ?? "Unknown"
            });
        }

        public async Task AddAsync(GradeVM vm)
        {
            var grade = new Grade
            {
                Value = vm.Value,
                SessionId = vm.SessionId,
                TraineeId = vm.TraineeId
            };

            await _gradeRepo.AddAsync(grade);
            await _gradeRepo.SaveAsync();
        }

        public async Task UpdateAsync(GradeVM vm)
        {
            var grade = await _gradeRepo.GetByIdAsync(vm.Id);
            grade.Value = vm.Value;
            grade.SessionId = vm.SessionId;
            grade.TraineeId = vm.TraineeId;

            _gradeRepo.Update(grade);
            await _gradeRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var grade = await _gradeRepo.GetByIdAsync(id);
            _gradeRepo.Delete(grade);
            await _gradeRepo.SaveAsync();
        }
    }
}
