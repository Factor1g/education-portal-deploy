using Model;

namespace EducationPortal.Web.Models
{
    public class CourseListViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public List<int> UserEnrolledCourses { get; set; }
    }
}
