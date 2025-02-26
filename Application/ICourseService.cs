using Model;

namespace Application
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course GetById(int id);
        void CreateCourse(Course course);         
        void Update(Course course);
        void Delete(int id);
    }
}

