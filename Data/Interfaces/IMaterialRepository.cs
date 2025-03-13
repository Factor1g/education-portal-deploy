using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Interfaces
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<List<Material>> GetCompletedMaterials(int userId);
        Task<bool> CompleteMaterial(int userId, int materialId);
    }
}
