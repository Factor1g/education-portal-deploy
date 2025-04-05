using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Exceptions;
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
        private readonly IMaterialService _materialService;

        public CourseService(ICourseRepository courseRepository, IMaterialRepository materialRepository, ISkillRepository skillRepository, IMaterialService materialService)
        {
            _courseRepository = courseRepository;
            _materialRepository = materialRepository;
            _skillRepository = skillRepository;
            _materialService = materialService;
        }

        public async Task CreateCourse(Course course)
        {
            await _courseRepository.Insert(course);
        }     

        public async Task Delete(int id)
        {
            await _courseRepository.Delete(id);
        }
        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCourses();
        }        

        public Task<Course> GetById(int id)
        {
            return  _courseRepository.GetById(id);
        }

        public async Task<List<Course>> GetCompletedCourses(string userId)
        {
            return await _courseRepository.GetCompletedCourses(userId);
        }

        public async Task<List<Course>> GetInProgressCourses(string userId)
        {
            return await _courseRepository.GetInProgressCourses(userId);
        }

        public async Task Update(Course course)
        {
            await _courseRepository.Update(course);
        }

        public async Task Update(int id, string name, string description, List<Material> materials, List<Skill> skills)
        {
            var course = await _courseRepository.GetById(id);
            if (course == null) throw new Exception("Course not found");

            course.Name = name;
            course.Description = description;

            await _courseRepository.Update(course, materials, skills);
        }

        public async Task<List<Material>> GetAllCourseMaterials(int courseId)
        {
            return await _courseRepository.GetAllCourseMaterials(courseId);
        }

        public async Task<List<Skill>> GetAllCourseSkills(int courseId)
        {
            return await _courseRepository.GetAllCourseSkills(courseId);
        }

        public async Task<bool> EnrollInCourse(string userId, int courseId)
        {
            var courseMaterials = await GetAllCourseMaterials(courseId);
            var completedMaterials = await _materialService.GetCompletedMaterials(userId);
            if (courseMaterials.All(m => completedMaterials.Contains(m)))
            {
                return await AddCompletedCourse(userId, courseId);                
            }
            
            return await _courseRepository.EnrollInCourse(userId, courseId);
        }
        public async Task<bool> AddCompletedCourse(string userId, int courseId)
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
            }
            if (material == null)
            {
                throw new MaterialNotFoundException("Material not found!");                
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

        public async Task<List<int>> SubscribedCourseIds(string userId)
        {
            var inProgress = await GetInProgressCourses(userId);
            var completed = await GetCompletedCourses(userId);

            return inProgress.Select(c => c.Id)
                .Union(completed.Select(c => c.Id))
                .ToList();
        }

        public async Task<int> GetCourseCompletionPercentage(Course course, string userId)
        {            
            var completedMaterials = await _materialService.GetCompletedMaterials(userId);
            var courseMaterials = await GetAllCourseMaterials(course.Id);
            var completedCount = completedMaterials.Count(m => courseMaterials.Contains(m));
            var totalCount = courseMaterials.Count;
            return totalCount > 0 ? (int)((double)completedCount / totalCount * 100) : 0;
        }
    }
}
