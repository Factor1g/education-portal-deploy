using Data;
using Data.Interfaces;
using EducationPortal.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Application
{
    public class UserService : IUserService
    {
        
        
        private IUserRepository _userRepository;
        private ICourseRepository _courseRepository;
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public UserService( IUserRepository userRepository, ICourseRepository courseRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {            
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<User> GetById(string userId)
        {
            return await _userRepository.GetById(userId);
        }

        public async Task CompleteCourse(User user, Course course)
        {           
            user.CompletedCourses.Add(course);
        }
       
        public void EnrollInCourse(User user, Course course)
        {
            if (!user.InProgressCourses.Contains(course))
            {
                user.InProgressCourses.Add(course);
            }
        }

        public async Task<IdentityResult> RegisterUser(string username,  string password, string role)
        {
            var normalizedRole = role == Roles.Teacher ? Roles.Teacher : Roles.Student;
            var user = new User
            {
                UserName = username,
                Role = normalizedRole
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, normalizedRole);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }            

            return result;
        }


    }
}
