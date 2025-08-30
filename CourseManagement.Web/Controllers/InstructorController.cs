using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Web.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
