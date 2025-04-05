using Application;
using EducationPortal.Model;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace EducationPortal.Web.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly UserManager<User> _userManager;

        public SkillController(ISkillService skillService, UserManager<User> userManager)
        {
            _skillService = skillService;
            _userManager = userManager;
        }

        [HttpGet]        
        public async Task<IActionResult> Index()
        {
            var skills = await _skillService.GetAllSkills();
            var user = await _userManager.GetUserAsync(User);

            SkillListViewModel model = new SkillListViewModel
            {
                Skills = skills.ToList(),
                CurrentUserId = user.Id,
                IsTeacher = await _userManager.IsInRoleAsync(user, Roles.Teacher)
            };

            return View(model);
        }
  
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Skill model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            model.SkillCreatorId = user.Id;
            model.SkillCreator = user;

            await _skillService.CreateSkill(model);
            return RedirectToAction("Index","Skill");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkillInLine(Skill model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            model.SkillCreatorId = user.Id;
            model.SkillCreator = user;

            await _skillService.CreateSkill(model);
            return RedirectToAction("Create", "Course");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _skillService.GetById(id);
            if (skill == null) return NotFound();

            return View(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Skill model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.GetUserAsync(User);
            model.SkillCreatorId = user.Id;
            model.SkillCreator = user;
            await _skillService.Update(model);
            return RedirectToAction("Index");
        }

        [HttpPost]  
        public async Task<IActionResult> Delete(int id)
        {
            await _skillService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
