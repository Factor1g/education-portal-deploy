using Data;
using Data.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISkillRepository _skillRepository;

        public MaterialService(IMaterialRepository materialRepository, IUserRepository userRepository, ICourseRepository courseRepository, ISkillRepository skillRepository)
        {
            _materialRepository = materialRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _skillRepository = skillRepository;
        }

        public async Task CreateMaterial(Material material)
        {
            await _materialRepository.Insert(material);
        }

        public async Task Delete(int id)
        {
            await _materialRepository.Delete(id);
        }

        public async Task<IEnumerable<Material>> GetAllMaterials()
        {
            return await _materialRepository.GetAll();
        }

        public Task<Material> GetById(int id)
        {
            return _materialRepository.GetById(id);
        }

        public async Task UpdateMaterial(Material material)
        {
            await _materialRepository.Update(material);
        }

        public async Task CompleteMaterial(string userId, int materialId)
        {
            var material = await _materialRepository.GetById(materialId);
            var completedMaterials = await _materialRepository.GetCompletedMaterials(userId);           
            
            if (!completedMaterials.Contains(material))
            {
                await _materialRepository.CompleteMaterial(userId, materialId);
                completedMaterials.Add(material);
            }            

            foreach (var course in await _courseRepository.GetInProgressCourses(userId))
            {
                var courseMaterials = await _courseRepository.GetAllCourseMaterials(course.Id);
                if (courseMaterials.All(m => completedMaterials.Contains(m)))
                {
                    if (!(await _courseRepository.GetCompletedCourses(userId)).Contains(course))
                    {
                        await _courseRepository.AddCompletedCourse(userId, course.Id);

                        foreach (var skill in await _courseRepository.GetAllCourseSkills(course.Id))
                        {                            
                            await _skillRepository.AcquireSkill(userId, skill.Id);
                        }
                    }
                }
            }

        }

        public async Task<List<Material>> GetCompletedMaterials(string userId)
        {
            return await _materialRepository.GetCompletedMaterials(userId);
        }              
    }
}
