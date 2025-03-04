using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUserService
    {
        void Register(string username, string password);
        User Login (string username, string password);
        Task<User> GetById(int userId);
        void DisplaySkills(User user);        
        void EnrollInCourse(User user, Course course);               
    }
}
