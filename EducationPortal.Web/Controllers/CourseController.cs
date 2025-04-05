using Application;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using EducationPortal.Model;

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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);            
            var courses = await _courseService.GetAllCourses();
            var inProgress = await _courseService.GetInProgressCourses(user.Id);
            var completed = await _courseService.GetCompletedCourses(user.Id);

            var ViewModel = new CourseViewModel
            {
                Courses = courses,
                CurrentUserId = user.Id,
                IsTeacher = await _userManager.IsInRoleAsync(user, Roles.Teacher),                
                SubscribedCourseIds = await _courseService.SubscribedCourseIds(user.Id),
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
                await PopulateDropdowns(model);
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            var selectedMaterialIds = model.SelectedMaterialIds;
            var selectedSkillIds= model.SelectedSkillIds;

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,                
                Materials = await _materialService.GetSelectedMaterials(selectedMaterialIds),                
                Skills = await _skillService.GetSelectedSkills(selectedSkillIds),
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
                await _courseService.EnrollInCourse(user.Id, courseId);
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

            var selectedMaterialIds = model.SelectedMaterialIds;
            var selectedSkillIds = model.SelectedSkillIds;
          
            var updatedMaterials = await _materialService.GetSelectedMaterials(selectedMaterialIds);
            var updatedSkills = await _skillService.GetSelectedSkills(selectedSkillIds);
           
            await _courseService.Update(model.Id, model.Name, model.Description, updatedMaterials, updatedSkills);
            return RedirectToAction("Index");

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
