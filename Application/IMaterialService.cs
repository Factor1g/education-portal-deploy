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
        Task CreateMaterial(Material material);
        Task UpdateMaterial(Material material);
        Task DeleteMaterial(int id);
        Task<IEnumerable<Material>> GetAllMaterials();
        Task<Material> GetById(int id);
        
        Task CompleteMaterial(int userId, int materialId);
        Task<List<Material>> GetCompletedMaterials(int userId);
    }
}
