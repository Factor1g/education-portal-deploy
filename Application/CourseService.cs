using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
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
        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCourses();
        }        

        public Task<Course> GetById(int id)
        {
            return  _courseRepository.GetById(id);
        }

        public async Task<List<Course>> GetCompletedCourses(int userId)
        {
            return await _courseRepository.GetCompletedCourses(userId);
        }

        public async Task<List<Course>> GetInProgressCourses(int userId)
        {
            return await _courseRepository.GetInProgressCourses(userId);
        }

        public async Task Update(Course course)
        {
            await _courseRepository.Update(course);
        }

        
    }
}
