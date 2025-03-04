using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<Course>> GetAllCourses();
        Task<List<Course>> GetInProgressCourses(int userId);
        Task<List<Course>> GetCompletedCourses(int userId);
    }
}
