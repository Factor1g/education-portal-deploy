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

        public MaterialService(IMaterialRepository materialRepository, IUserRepository userRepository)
        {
            _materialRepository = materialRepository;
            _userRepository = userRepository;
        }

        public void CreateMaterial(Material material)
        {
            _materialRepository.Insert(material);
        }

        public void DeleteMaterial(int id)
        {
            _materialRepository.Delete(id);
        }

        public async Task<IEnumerable<Material>> GetAllMaterials()
        {
            return await _materialRepository.GetAll();
        }

        public Task<Material> GetMaterial(int id)
        {
            return _materialRepository.GetById(id);
        }

        public void UpdateMaterial(Material material)
        {
            _materialRepository.Update(material);
        }

        public async Task CompleteMaterial(User user, int materialId)
        {
            var material = await _materialRepository.GetById(materialId);
            if (material == null)
            {
                throw new MaterialNotFoundException("Material could not be found!");
            }
            
            if (!user.CompletedMaterials.Contains(material))
            {
                user.CompletedMaterials.Add(material);
                await _userRepository.Update(user);                
            }
            else
            {
                throw new MaterialAlreadyCompletedException("Material already completed!");
            }
        }

        public async Task<List<Material>> GetCompletedMaterials(int userId)
        {
            return await _materialRepository.GetCompletedMaterials(userId);
        }
    }
}
