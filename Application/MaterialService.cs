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

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public void CreateMaterial(Material material)
        {
            _materialRepository.Insert(material);
        }

        public void DeleteMaterial(int id)
        {
            _materialRepository.Delete(id);
        }

        public Material GetMaterial(int id)
        {
            return _materialRepository.GetById(id);
        }

        public void UpdateMaterial(Material material)
        {
            _materialRepository.Update(material);
        }
    }
}
