using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using EducationPortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Model;

namespace EducationPortal.Web.Controllers
{
    public class AuthController : Controller
    {     
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

            var role = model.Role == "Teacher" ? "Teacher" : "Student";

            var user = new User { UserName = model.Username, Role = role };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                System.Console.WriteLine("User created!");
                await _userManager.AddToRoleAsync(user, role);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Profile", "User");
            }

            foreach (var error in result.Errors)
            {
                System.Console.WriteLine($"Error: {error.Description}" );

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
            if (user == null)
            {
                System.Console.WriteLine("User not found in database!");
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            System.Console.WriteLine($"User found: {user.UserName}");

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                HttpContext.Session.SetString("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.UserName);
                System.Console.WriteLine("Successful login!");
                return RedirectToAction("Profile", "User");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
