
﻿using CourseManagement.BLL.Interfaces;
using CourseManagement.BLL.Services.Interfaces;
using CourseManagement.BLL.ViewModels;
using CourseManagement.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace CourseManagement.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public CourseController(AppDbContext context, ICourseService courseService, IUserService userService)
        {
            _context = context;
            _courseService = courseService;
            _userService = userService;
        }


        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 5)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    Category = c.Category,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    InstructorId = c.InstructorId ?? 0
                })
                .ToListAsync();

            var vm = new CourseListVM
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


        private int? ReadPageSizeFromCookie()
        {
            if (Request.Cookies.TryGetValue("CoursePageSize", out var value) &&
                int.TryParse(value, out int size))
            {
                return size;
            }
            return null;
        }

        private void WritePageSizeCookie(int pageSize)
        {
            Response.Cookies.Append("CoursePageSize",
                pageSize.ToString(),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) });
        }

        // Create GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Instructors = new SelectList(await _userService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVM model)
        {
            if (ModelState.IsValid)
            {
                await _courseService.AddAsync(model);
                TempData["Success"] = "Course created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Instructors = new SelectList(await _userService.GetAllAsync(), "Id", "Name", model.InstructorId);
            return View(model);
        }

        // Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();

            ViewBag.Instructors = new SelectList(await _userService.GetAllAsync(), "Id", "Name", course.InstructorId);
            return View(course);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseVM model)
        {
            if (ModelState.IsValid)
            {
                await _courseService.UpdateAsync(model);
                TempData["Success"] = "Course updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Instructors = new SelectList(await _userService.GetAllAsync(), "Id", "Name", model.InstructorId);
            return View(model);
        }

        // Delete GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                TempData["Error"] = "Course not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _courseService.DeleteAsync(id);
                TempData["Success"] = "Course deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting course: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // Validation: Unique name
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsCourseNameUnique(string name, int? id)
        {
            var courses = await _courseService.SearchByNameAsync(name);
            bool isUnique = !courses.Any(c => c.Id != id);
            return Json(isUnique);
        }
    }

}
              
           
