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

        public void CompleteCourse(User user, Course course)
        {
            throw new NotImplementedException();
        }

        public void CompleteMaterial(User user, string materialTitle)
        {
            throw new NotImplementedException();
        }

        public void CreateCourse(Course course)
        {
            _courseRepository.Insert(course);
        }

        public void CreateMaterial(Material material)
        {
            throw new NotImplementedException();
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

        /* --- DONE FOR EFCORE --- */
        public User Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
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
    }
}
