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
                System.Console.WriteLine("User not found in DB!");
                return NotFound();
            }

            var viewModel = new UserProfileViewModel
            {
                Username = user.UserName,
                EnrolledCourses = await _courseService.GetInProgressCourses(user.Id),
                CompletedCourses = await _courseService.GetCompletedCourses(user.Id),
                Skills = await _skillService.GetUserSkills(user.Id),
                CompletedMaterials = await _materialService.GetCompletedMaterials(user.Id)
            };

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

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
