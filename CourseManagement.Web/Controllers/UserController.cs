using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 5)
        {
            var usersQuery = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                usersQuery = usersQuery.Where(u => u.Name.Contains(search));

            var totalItems = await usersQuery.CountAsync();

            var items = await usersQuery
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role 
                })
                .ToListAsync();

            var vm = new UserListVM
            {
                Items = items,
                Search = search,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };

            return View(vm); 
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = vm.Name,
                    Email = vm.Email,
                    Role = vm.Role
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                TempData["success"] = "User created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            var vm = new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
            return View(vm);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserVM vm)
        {
            if (id != vm.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return NotFound();

                user.Name = vm.Name;
                user.Email = vm.Email;
                user.Role = vm.Role;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                TempData["success"] = "User updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();

            var vm = new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
            return View(vm);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["success"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // Remote Validation: Email unique
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailUnique(string email, int id = 0)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == email && u.Id != id);
            if (exists)
                return Json($"Email '{email}' is already taken.");

            return Json(true);
        }
    }
}
