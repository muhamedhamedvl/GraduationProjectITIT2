using CourseManagement.BLL.Interfaces;
using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CourseManagement.Web.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ICourseService _courseService;


        public SessionController(ISessionService sessionService, ICourseService courseService)
        {
            _sessionService = sessionService;
            _courseService = courseService;
        }


        public async Task<IActionResult> Index(int? courseId, string? search, int page = 1, int pageSize = 5)
        {
            var sessions = await _sessionService.GetAllAsync();

            if (courseId.HasValue)
                sessions = sessions.Where(s => s.CourseId == courseId.Value);

            if (!string.IsNullOrEmpty(search))
                sessions = sessions.Where(s => s.CourseName.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalItems = sessions.Count();
            var items = sessions
                .OrderBy(s => s.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new SessionListVM
            {
                Items = items,
                Search = search,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };

            ViewBag.CourseId = courseId;

            return View(vm);
        }

        // Create GET
        [HttpGet]
        public async Task<IActionResult> Create(int? courseId)
        {
            await LoadCoursesDropDown(courseId);
            var vm = new SessionVM();
            if (courseId.HasValue) vm.CourseId = courseId.Value;
            return View(vm);
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionVM model)
        {
            if (ModelState.IsValid)
            {
                if (!await IsSessionWithinCoursePeriod(model))
                {
                    ModelState.AddModelError("", "Session dates must be within the course duration.");
                    await LoadCoursesDropDown(model.CourseId);
                    return View(model);
                }

                await _sessionService.AddAsync(model);
                TempData["Success"] = "Session created successfully!";
                return RedirectToAction(nameof(Index), new { courseId = model.CourseId });
            }

            await LoadCoursesDropDown(model.CourseId);
            return View(model);
        }

        // Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null) return NotFound();

            await LoadCoursesDropDown(session.CourseId);
            return View(session);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SessionVM model)
        {
            if (ModelState.IsValid)
            {
                if (!await IsSessionWithinCoursePeriod(model))
                {
                    ModelState.AddModelError("", "Session dates must be within the course duration.");
                    await LoadCoursesDropDown(model.CourseId);
                    return View(model);
                }

                await _sessionService.UpdateAsync(model);
                TempData["Success"] = "Session updated successfully!";
                return RedirectToAction(nameof(Index), new { courseId = model.CourseId });
            }

            await LoadCoursesDropDown(model.CourseId);
            return View(model);
        }

        // Delete GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                TempData["Error"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null)
            {
                TempData["Error"] = "Session not found.";
                return RedirectToAction(nameof(Index));
            }

            await _sessionService.DeleteAsync(id);
            TempData["Success"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index), new { courseId = session.CourseId });
        }

        // Remote Validation: EndDate > StartDate
        [AcceptVerbs("GET", "POST")]
        public IActionResult ValidateEndDate(DateTime endDate, DateTime startDate)
        {
            if (endDate <= startDate) return Json(false);
            return Json(true);
        }

        // Helpers
        private async Task LoadCoursesDropDown(int? selectedCourseId = null)
        {
            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", selectedCourseId);
        }

        private async Task<bool> IsSessionWithinCoursePeriod(SessionVM session)
        {
            var course = await _courseService.GetByIdAsync(session.CourseId);
            if (course == null) return false;
            return session.StartDate >= course.StartDate && session.EndDate <= course.EndDate;
        }
    }
}
