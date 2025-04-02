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
        void EnrollInCourse(User user, Course course);        
    }
}
