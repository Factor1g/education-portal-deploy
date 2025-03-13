using Model;

namespace Application
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCourses();
        Task<Course> GetById(int id);
        void CreateCourse(Course course);         
        Task Update(Course course);
        void Delete(int id);
        Task<List<Course>> GetInProgressCourses(int userId);
        Task<List<Course>> GetCompletedCourses(int userId);
        Task<bool> EnrollInCourse(int userId, int courseId);
        Task<bool> AddCompletedCourse(int userId, int courseId);
        Task AddMaterialToCourse(int courseId, int materialId);
        Task AddSkillToCourse(int courseId, int skillId);
    }
}

