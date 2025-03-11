using Application;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // ✅ Display all available courses
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var courses = await _courseService.GetAllCourses();
            var userCourses = await _courseService.GetInProgressCourses(int.Parse(userId));

            var viewModel = new CourseListViewModel
            {
                Courses = courses,
                UserEnrolledCourses = userCourses.Select(c => c.Id).ToList()
            };

            return View(viewModel);
        }

        // ✅ Subscribe a user to a course
        public async Task<IActionResult> Subscribe(int courseId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var enrolled = await _courseService.EnrollInCourse(int.Parse(userId), courseId);
            if (!enrolled)
            {
                TempData["Message"] = "You are already enrolled in this course.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
