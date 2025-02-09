using Model;

namespace Application
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourse(string name);
        void CreateCourse(Course course);         
        void UpdateCourse(Course course);
        void DeleteCourse(string name);
    }
}

