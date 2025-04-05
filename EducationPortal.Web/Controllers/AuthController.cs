using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Model;
using EducationPortal.Model;


namespace EducationPortal.Web.Controllers
{
    public class AuthController : Controller
    {     
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult Register() => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            System.Console.WriteLine("Register method called!");
            if (!ModelState.IsValid)
            {
                System.Console.WriteLine("Model state is invalid!");
                return View(model);
            }
           
            var result = await _userService.RegisterUser(model.Username, model.Password, model.Role);

            if (result.Succeeded)
            {
                return RedirectToAction("Profile", "User");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Login() => View();
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                System.Console.WriteLine("Login method called!");
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);           
            
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded || user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
            HttpContext.Session.SetString("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.UserName);
            System.Console.WriteLine("Successful login!");
            return RedirectToAction("Profile", "User");            
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
