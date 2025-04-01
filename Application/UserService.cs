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
    }
}
