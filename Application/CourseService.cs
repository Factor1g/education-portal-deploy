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
        private readonly IMaterialRepository _materialRepository;
        private readonly ISkillRepository _skillRepository;

        public CourseService(ICourseRepository courseRepository, IMaterialRepository materialRepository, ISkillRepository skillRepository)
        {
            _courseRepository = courseRepository;
            _materialRepository = materialRepository;
            _skillRepository = skillRepository;
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

        public async Task<bool> EnrollInCourse(int userId, int courseId)
        {
            return await _courseRepository.EnrollInCourse(userId, courseId);
        }
        public async Task<bool> AddCompletedCourse(int userId, int courseId)
        {
            return await _courseRepository.AddCompletedCourse(userId, courseId);
        }

        public async Task AddMaterialToCourse(int courseId, int materialId)
        {
            var course = await _courseRepository.GetById(courseId);
            var material = await _materialRepository.GetById(materialId);

            if (course == null)
            {
                throw new CourseNotFoundException("Course not found!");
                return;
            }
            if (material == null)
            {
                throw new MaterialNotFoundException("Material not found!");
                return;
            }

            if (!course.Materials.Contains(material))
            {
                course.Materials.Add(material);
                await _courseRepository.Update(course);                  
            }
            else
            {
                throw new InvalidOperationException("Invalid operation!");
            }
        }

        public async Task AddSkillToCourse(int courseId, int skillId)
        {
            var course = await _courseRepository.GetById(courseId);
            var skill = await _skillRepository.GetById(skillId);

            if (course == null)
            {
                throw new CourseNotFoundException("No course or skill was found with the given ID!");
            }
            if (skill == null)
            {
                throw new MaterialNotFoundException("Material not found!");
                return;
            }
            if (!course.Skills.Contains(skill))
            {
                course.Skills.Add(skill);
                await _courseRepository.Update(course);
            }
            else
            {
                throw new InvalidOperationException("Invalid operation!");
            }
        }

    }
}
