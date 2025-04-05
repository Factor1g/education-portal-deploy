using Model;

namespace Application
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCourses();
        Task<Course> GetById(int id);
        Task CreateCourse(Course course);         
        Task Update(Course course);
        Task Update(int id, string name, string description, List<Material> materials, List<Skill> skills);
        Task Delete(int id);
        Task<List<Material>> GetAllCourseMaterials(int courseId);
        Task<List<Skill>> GetAllCourseSkills(int courseId);
        Task<List<Course>> GetInProgressCourses(string userId);
        Task<List<Course>> GetCompletedCourses(string userId);
        Task<bool> EnrollInCourse(string userId, int courseId);
        Task<bool> AddCompletedCourse(string userId, int courseId);
        Task AddMaterialToCourse(int courseId, int materialId);
        Task AddSkillToCourse(int courseId, int skillId);
        Task<int> GetCourseCompletionPercentage(Course course, string userId);
        Task<List<int>> SubscribedCourseIds(string userId);
    }
}

