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
        Task<List<Course>> GetInProgressCourses(string userId);
        Task<List<Course>> GetCompletedCourses(string userId);
        Task<bool> EnrollInCourse(string userId, int courseId);
        Task<List<Material>> GetAllCourseMaterials(int courseId);
        Task<bool> AddCompletedCourse(string userId, int courseId);
        Task<List<Skill>> GetAllCourseSkills(int courseId);
    }
}
