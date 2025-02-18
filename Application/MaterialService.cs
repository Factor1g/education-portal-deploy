using Data;
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
        private readonly IDataRepository _dataRepository;

        public MaterialService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void CreateMaterial(Material material)
        {
            _dataRepository.AddMaterial(material);
        }

        public void DeleteMaterial(string title)
        {
            _dataRepository.DeleteMaterial(title);
        }

        public Material GetMaterial(string title)
        {
            return _dataRepository.GetMaterial(title);
        }

        public void UpdateMaterial(Material material)
        {
            _dataRepository.UpdateMaterial(material);
        }
    }
}
