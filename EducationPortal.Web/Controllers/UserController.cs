using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Model;
using Microsoft.AspNetCore.Authorization;

namespace EducationPortal.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly ISkillService _skillService;
        private readonly IMaterialService _materialService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, ICourseService courseService, ISkillService skillService, IMaterialService materialService, UserManager<User> userManager)
        {
            _userService = userService;
            _courseService = courseService;
            _skillService = skillService;
            _materialService = materialService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                System.Console.WriteLine("Login method called!");
                return RedirectToAction("Login", "Auth");
            }

            System.Console.WriteLine("User authenticated!");

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = await _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var enrolledCourses = await _courseService.GetInProgressCourses(user.Id);

            var viewModel = new UserProfileViewModel
            {
                Username = user.UserName,
                EnrolledCourses = enrolledCourses,
                CompletedCourses = await _courseService.GetCompletedCourses(user.Id),
                Skills = await _skillService.GetUserSkills(user.Id),
                CompletedMaterials = await _materialService.GetCompletedMaterials(user.Id)
                
            };

            ViewBag.CourseProgress = new Dictionary<int, int>();
            foreach (var course in enrolledCourses)
            {
                ViewBag.CourseProgress[course.Id] = await _courseService.GetCourseCompletionPercentage(course, user.Id);
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CompleteMaterial(int materialId)
        {
            var userId = _userManager.GetUserId(User);
            await _materialService.CompleteMaterial(userId, materialId);
            return RedirectToAction("Profile");
        }
    }
}
