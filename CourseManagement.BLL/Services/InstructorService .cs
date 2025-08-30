using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Interfaces;
using CourseManagement.DAL.Models;

namespace CourseManagement.BLL.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;

        public InstructorService(IInstructorRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
