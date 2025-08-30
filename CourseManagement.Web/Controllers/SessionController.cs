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

        
        public async Task<IActionResult> Index(string courseName, int page = 1)
        {
            var sessions = await _sessionService.GetAllSessionsAsync(courseName, page);
            ViewBag.CourseName = courseName;
            return View(sessions);
        }

        
        public async Task<IActionResult> Create()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sessionService.AddSessionAsync(model);
                    TempData["Success"] = "Session created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", model.CourseId);
            return View(model);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
                return NotFound();

            var model = new SessionViewModel
            {
                Id = session.Id,
                Name = session.Name,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseId = session.CourseId
            };

            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", model.CourseId);

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SessionViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    await _sessionService.UpdateSessionAsync(id, model);
                    TempData["Success"] = "Session updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name", model.CourseId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sessionService.DeleteSessionAsync(id);
            TempData["Success"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
