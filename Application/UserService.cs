using Data;
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
        
        private IDataRepository _dataRepository;

        public UserService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;            
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
            _dataRepository.AddCourse(course);
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

        public User Login(string username, string password)
        {
            var user = _dataRepository.GetUser(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }
        public void Register(string username, string password)
        {
            var existingUser = _dataRepository.GetUser(username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username is already in use!");
            }
            else
            {
                var user = new User{ Username = username, Password = password};
                _dataRepository.AddUser(user);
            }
        }
    }
}
