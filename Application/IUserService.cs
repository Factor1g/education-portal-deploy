using Microsoft.AspNetCore.Identity;
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
        Task<User> GetById(string userId);
        Task<IdentityResult> RegisterUser(string username, string password, string role);
        void EnrollInCourse(User user, Course course);        
    }
}
