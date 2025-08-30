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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<UserVM>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();

            return users.Select(u => new UserVM
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });
        }

        public async Task<UserVM?> GetByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            return new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<UserVM?> GetByEmailAsync(string email)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return null;

            return new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task AddAsync(UserVM vm)
        {
            var user = new User
            {
                Name = vm.Name,
                Email = vm.Email,
                Role = vm.Role
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();
        }

        public async Task UpdateAsync(UserVM vm)
        {
            var user = await _userRepo.GetByIdAsync(vm.Id);
            if (user == null) return;

            user.Name = vm.Name;
            user.Email = vm.Email;
            user.Role = vm.Role;

            _userRepo.Update(user);
            await _userRepo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return;

            _userRepo.Delete(user);
            await _userRepo.SaveAsync();
        }
    }
}
