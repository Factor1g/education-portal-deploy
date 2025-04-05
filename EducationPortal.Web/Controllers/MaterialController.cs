using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EducationPortal.Model;

namespace EducationPortal.Web.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;
        private readonly ILogger<MaterialController> _logger;
        private readonly UserManager<User> _userManager;

        public MaterialController(IMaterialService materialService, ILogger<MaterialController> logger, UserManager<User> userManager)
        {
            _materialService = materialService;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var material = await _materialService.GetById(id);

            if (material == null)
            {
                return NotFound();
            }

            var model = new MaterialCreateViewModel
            {
                Id = material.Id,
                Title = material.Title,
                Description = material.Description,
                Type = material.GetType().Name,
                Author = (material as Book)?.Author,
                Pages = (material as Book)?.Pages,
                Format = (material as Book)?.Format,
                Year = (material as Book)?.Year,
                Duration = (material as Video)?.Duration,
                Quality = (material as Video)?.Quality,
                PublicationDate = (material as Article)?.PublicationDate,
                Resource = (material as Article)?.Resource,
            };

            return View("Edit", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(MaterialCreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var material = await _materialService.GetById(model.Id);
            if (material == null) return NotFound();

            MapViewModelToExistingMaterial(material, model);

            await _materialService.UpdateMaterial(material);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateInline(MaterialCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MaterialError"] = "Please correct errors in material form.";

                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) });

                foreach (var error in errors)
                {
                    System.Console.WriteLine($"[ModelState Error] Field: {error.Field}");
                    foreach (var message in error.Errors)
                    {
                        System.Console.WriteLine($" - {message}");
                    }
                }

                return RedirectToAction("Create", "Course");
            }            
            Material material = null;

            MapViewModelToMaterial(ref material, model);

            await _materialService.CreateMaterial(material);
            return RedirectToAction("Create", "Course");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var materials = await _materialService.GetAllMaterials();
            var user = await _userManager.GetUserAsync(User);

            var viewModel = new MaterialListViewModel
            {
                Materials = materials.ToList(),
                CurrentUserId = user.Id,
                IsTeacher = await _userManager.IsInRoleAsync(user, Roles.Teacher)
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MaterialCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Material material = null;

            MapViewModelToMaterial(ref material, model);

            var user = await _userManager.GetUserAsync(User);
            material.CreatorId = user.Id;

            await _materialService.CreateMaterial(material);
            return RedirectToAction("Index", "Material");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _materialService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var material = await _materialService.GetById(id);
            if (material == null) return NotFound();

            return View(material);
        }

        private void MapViewModelToExistingMaterial(Material material, MaterialCreateViewModel model)
        {
            switch (material)
            {
                case Video video:
                    video.Duration = model.Duration ?? 0;
                    video.Quality = model.Quality;
                    break;

                case Book book:
                    book.Author = model.Author;
                    book.Pages = model.Pages ?? 0;
                    book.Format = model.Format;
                    book.Year = model.Year ?? 0;
                    break;

                case Article article:
                    article.PublicationDate = model.PublicationDate ?? DateTime.Now;
                    article.Resource = model.Resource;
                    break;
            }
            material.Title = model.Title;
            material.Description = model.Description;
        }

        private void MapViewModelToMaterial(ref Material material, MaterialCreateViewModel model)
        {
            switch (model.Type)
            {
                case "Video":
                    material = new Video
                    {
                        Duration = model.Duration ?? 0,
                        Quality = model.Quality
                    };
                    break;

                case "Book":
                    material = new Book
                    {
                        Author = model.Author,
                        Pages = model.Pages ?? 0,
                        Format = model.Format,
                        Year = model.Year ?? 0,
                    };
                    break;

                case "Article":
                    material = new Article
                    {
                        PublicationDate = model.PublicationDate ?? DateTime.Now,
                        Resource = model.Resource
                    };
                    break;
            }
            material.Title = model.Title;
            material.Description = model.Description;
        }

    }
}