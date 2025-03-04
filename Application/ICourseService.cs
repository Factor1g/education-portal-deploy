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
    }
}

