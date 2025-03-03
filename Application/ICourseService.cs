using Model;

namespace Application
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetById(int id);
        void CreateCourse(Course course);         
        void Update(Course course);
        void Delete(int id);
    }
}

