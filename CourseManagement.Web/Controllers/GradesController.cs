using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entites;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class GradesController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;

        public GradesController(IGradeService gradeService, ISessionService sessionService, IUserService userService)
        {
            _gradeService = gradeService;
            _sessionService = sessionService;
            _userService = userService;
        }

        // GET: Grades 
        public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 5)
        {
            var grades = await _gradeService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                grades = grades.Where(g =>
                    (g.TraineeId != 0 &&
                     (_userService.GetByIdAsync(g.TraineeId).Result?.Name ?? "")
                        .Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                    (g.SessionId != 0 &&
                     (_sessionService.GetByIdAsync(g.SessionId).Result?.CourseName ?? "")
                        .Contains(searchString, StringComparison.OrdinalIgnoreCase))
                );
            }

            int totalItems = grades.Count();
            var pagedGrades = grades
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new GradeListVM
            {
                Items = pagedGrades,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                SearchString = searchString
            };

            return View(vm);
        }

        // GET: Grades/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Sessions = await _sessionService.GetAllAsync();
            ViewBag.Trainees = (await _userService.GetAllAsync()).Where(u => u.Role == Role.Trainee);
            return View();
        }

        // POST: Grades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeVM vm)
        {
            if (vm.SessionId == 0)
            {
                ModelState.AddModelError(nameof(vm.SessionId), "Please select a session (by Id).");
            }
            if (vm.TraineeId == 0)
            {
                ModelState.AddModelError(nameof(vm.TraineeId), "Please select a trainee.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Sessions = await _sessionService.GetAllAsync();
                ViewBag.Trainees = (await _userService.GetAllAsync()).Where(u => u.Role == Role.Trainee);
                return View(vm);
            }

            await _gradeService.AddAsync(vm);
            TempData["Success"] = "Grade recorded successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Grades/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null) return NotFound();

            ViewBag.Sessions = await _sessionService.GetAllAsync();
            ViewBag.Trainees = (await _userService.GetAllAsync()).Where(u => u.Role == Role.Trainee);
            return View(grade);
        }

        // POST: Grades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GradeVM vm)
        {
            if (id != vm.Id) return BadRequest();

            if (vm.SessionId == 0)
            {
                ModelState.AddModelError(nameof(vm.SessionId), "Please select a session (by Id).");
            }
            if (vm.TraineeId == 0)
            {
                ModelState.AddModelError(nameof(vm.TraineeId), "Please select a trainee.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Sessions = await _sessionService.GetAllAsync();
                ViewBag.Trainees = (await _userService.GetAllAsync()).Where(u => u.Role == Role.Trainee);
                return View(vm);
            }

            await _gradeService.UpdateAsync(vm);
            TempData["Success"] = "Grade updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gradeService.DeleteAsync(id);
            TempData["Success"] = "Grade deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TraineeGrades(int traineeId, int pageNumber = 1, int pageSize = 5)
        {
            var trainee = await _userService.GetByIdAsync(traineeId);
            if (trainee == null || trainee.Role != Role.Trainee) return NotFound();

            var grades = await _gradeService.GetByTraineeIdAsync(traineeId);

            int totalItems = grades.Count();
            var pagedGrades = grades
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TraineeName = trainee.Name;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(pagedGrades);
        }
    }
}
