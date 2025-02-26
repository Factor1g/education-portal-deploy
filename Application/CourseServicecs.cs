using Data;
using Data.Interfaces;
using Data.Repositories;
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
        private readonly ICourseRepository _courseRepository;

        public CourseServicecs(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void CreateCourse(Course course)
        {
            _courseRepository.Insert(course);
        }     

        public void Delete(int id)
        {
            _courseRepository.Delete(id);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }       

        public Course GetById(int id)
        {
            return _courseRepository.GetById(id);
        }       

        public void Update(Course course)
        {
            _courseRepository.Update(course);
        }
    }
}
