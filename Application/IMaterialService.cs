using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IMaterialService
    {
        void CreateMaterial(Material material);
        void UpdateMaterial(Material material);
        void DeleteMaterial(int id);
        Task<IEnumerable<Material>> GetAllMaterials();
        Task<Material> GetMaterial(int id);
        Task CompleteMaterial(User user, int materialId);
        Task<List<Material>> GetCompletedMaterials(int userId);
    }
}
