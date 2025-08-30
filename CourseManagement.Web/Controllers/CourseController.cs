
﻿using CourseManagement.BLL.Interfaces;
using CourseManagement.BLL.Services;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Migrations;
using CourseManagement.DAL.Entites;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System;

namespace CourseManagement.Web.Controllers
{
    public class CoursesController : Controller
    {


        private readonly ICourseService _courseService;
        private readonly AppDbContext _context;
        private readonly IInstructorService _instructorService;

        private const int PageSize = 5;

        public CoursesController(ICourseService courseService, AppDbContext context, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;

            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? category, int page = 1)
        {
            var total = await _courseService.GetCoursesCountAsync(search, category);
            var courses = await _courseService.GetPagedCoursesAsync(search, category, page, PageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / PageSize);
            ViewBag.Search = search;
            ViewBag.Category = category;

            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadInstructors();
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            //if (ModelState.IsValid)
            //{
            await _courseService.AddCourseAsync(course);
            return RedirectToAction(nameof(Index));
            //}

            // لو حصل خطأ Validation لازم نرجع instructors تاني
            await LoadInstructors();
            return View(course);
        }

        private async Task LoadInstructors()
        {
            var instructors = await _instructorService.GetAllAsync();

            ViewBag.Instructors = new SelectList(
                instructors.Select(i => new
                {
                    Id = i.Id,
                    FullName = i.FirstName + " " + i.LastName
                }),
                "Id", "FullName"
            );
        }






        // GET: Courses/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            await LoadInstructors(); // نرجع الـ instructors للـ dropdown
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            await _courseService.UpdateCourseAsync(course);
            return RedirectToAction(nameof(Index));
            //}

            // لو حصل خطأ Validation لازم نرجع instructors تاني
            await LoadInstructors();
            return View(course);
        }







        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            // استخدم GetAllAsync بدل GetInstructorAsync
            var instructors = await _instructorService.GetAllAsync();
            var instructor = instructors.FirstOrDefault(i => i.Id == course.InstructorId);

            ViewBag.InstructorName = instructor?.FirstName ?? "N/A";

            return View(course);
        }




        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseAsync(id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComplete(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
              
           
