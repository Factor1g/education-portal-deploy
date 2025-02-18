using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CourseServicecs : ICourseService
    {
        private readonly IDataRepository _dataRepository;

        public CourseServicecs(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void CreateCourse(Course course)
        {
            _dataRepository.AddCourse(course);
        }

        public void DeleteCourse(string name)
        {
            _dataRepository.DeleteCourse(name);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _dataRepository.GetAllCourses();
        }

        public Course GetCourse(string name)
        {
            return _dataRepository.GetCourse(name);
        }

        public void UpdateCourse(Course course)
        {
            _dataRepository.UpdateCourse(course);
        }
    }
}
