using Data;
using Data.Interfaces;
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

        public UserService( IUserRepository userRepository, ICourseRepository courseRepository)
        {            
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }
        
        public async Task<User> GetById(int userId)
        {
            return await _userRepository.GetById(userId);
        }

        public async Task CompleteCourse(User user, Course course)
        {           
            user.CompletedCourses.Add(course);
        }
        
        public void DisplaySkills(User user)
        {
            user.DisplaySkills();
        }
        public void EnrollInCourse(User user, Course course)
        {
            if (!user.InProgressCourses.Contains(course))
            {
                user.InProgressCourses.Add(course);
            }
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password)
            {
                throw new AuthorizationFailedException("Invalid credentials!");
            }
            return user;

        }
        public void Register(string username, string password)
        {
            var existingUser = _userRepository.GetUserByUsername(username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username is already in use!");
            }
            else
            {
                var user = new User{ Username = username, Password = password};
                _userRepository.Insert(user);
            }
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user =  _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password)
            {
                throw new Exception("Invalid credentials!");
            }
            return user;
        }
    }
}
