using Application;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EducationPortal.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISkillService _skillService;
        private readonly IMaterialService _materialService;
        private readonly UserManager<User> _userManager;

        public CourseController(ICourseService courseService, ISkillService skillService, IMaterialService material, UserManager<User> userManager)
        {
            _courseService = courseService;
            _skillService = skillService;
            _materialService = material;
            _userManager = userManager;
        }
        
        //public async Task<IActionResult> List()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var courses = await _courseService.GetAllCourses();
        //    var inProgress = await _courseService.GetInProgressCourses(user.Id);

        //    var viewModel = new CourseListViewModel
        //    {
        //        Courses = courses,
        //        CurrentUserId = user.Id,
        //        IsTeacher = await _userManager.IsInRoleAsync(user, "Teacher"),
        //        SubscribedCourseIds = inProgress.Select(c => c.Id).ToList()
        //    };

        //    return View(viewModel);
        //}

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);            
            var courses = await _courseService.GetAllCourses();
            var inProgress = await _courseService.GetInProgressCourses(user.Id);

            var ViewModel = new CourseViewModel
            {
                Courses = courses,
                CurrentUserId = user.Id,
                IsTeacher = await _userManager.IsInRoleAsync(user, "Teacher"),
                SubscribedCourseIds = inProgress.Select(c => c.Id).ToList(),
            };
            return View(ViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new CourseViewModel
            {
                AvailableMaterials = await _materialService.GetAllMaterials(),
                AvailableSkills = await _skillService.GetAllSkills()
                
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            System.Console.WriteLine("POST Create hit");
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        System.Console.WriteLine($"[Model Error] {key}: {error.ErrorMessage}");
                    }
                }
                await PopulateDropdowns(model);
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                Materials = (await _materialService.GetAllMaterials())
                    .Where(m => model.SelectedMaterialIds.Contains(m.Id)).ToList(),
                Skills = (await _skillService.GetAllSkills())
                    .Where(s => model.SelectedSkillIds.Contains(s.Id)).ToList(),                
                CreatorId = userId,
            };

            await _courseService.CreateCourse(course);
            return RedirectToAction("Index", "Course");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Subscribe(int courseId)
        {           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var course = await _courseService.GetById(courseId);
            if (course == null)
            {
                TempData["Message"] = "Course not found.";
                return RedirectToAction("List");
            }

            var alreadyEnrolled = (await _courseService.GetInProgressCourses(user.Id)).Any(c => c.Id == courseId);
            if (alreadyEnrolled)
            {
                TempData["Message"] = "Already enrolled!";
            }
            else
            {                
                user.InProgressCourses.Add(course);
                await _userManager.UpdateAsync(user);
                TempData["Message"] = "Enrolled successfully!";
            }
            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseViewModel
            {
                Id = id,
                Name = course.Name,
                Description = course.Description,
                SelectedMaterialIds = course.Materials.Select(m => m.Id).ToList(),
                SelectedSkillIds = course.Materials.Select(s => s.Id).ToList(),
                AvailableMaterials = await _materialService.GetAllMaterials(),
                AvailableSkills = await _skillService.GetAllSkills(),
                CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(model);
                return View(model);
            } 

            var course = await _courseService.GetById(model.Id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = model.Name;
            course.Description = model.Description;
            course.Materials = (await _materialService.GetAllMaterials())
                .Where(m => model.SelectedMaterialIds.Contains(m.Id)).ToList();
            course.Skills = (await _skillService.GetAllSkills())
                .Where(s => model.SelectedSkillIds.Contains(s.Id)).ToList();

            await _courseService.Update(course);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            var course = await _courseService.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseDetailViewModel
            {
                Course = course,
                Materials = await _courseService.GetAllCourseMaterials(course.Id),
                Skills = await _courseService.GetAllCourseSkills(course.Id),
            };

            return View(viewModel);
        }

        private async Task PopulateDropdowns(CourseViewModel model)
        {
            model.AvailableMaterials = await _materialService.GetAllMaterials();
            model.AvailableSkills = await _skillService.GetAllSkills();
        }
    }
}
