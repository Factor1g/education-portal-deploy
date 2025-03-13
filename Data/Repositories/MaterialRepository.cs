using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MaterialRepository : EfBaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(EducationPortalContext context) : base(context)
        {
        }

        public async Task<List<Material>> GetCompletedMaterials(int userId)
        {
            return await context.Set<User>()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.CompletedMaterials)
                .ToListAsync();
        }

        public async Task<bool> CompleteMaterial(int userId, int materialId)
        {
            var user = await context.Set<User>().Include(u => u.CompletedMaterials).FirstOrDefaultAsync(u => u.Id == userId);
            var material = await context.Set<Material>().FirstOrDefaultAsync(m => m.Id == materialId);

            if (user == null || material == null)
            {
                throw new MaterialNotFoundException("No material was found with given ID!");
                return false;
            }
            if (!user.CompletedMaterials.Contains(material))
            {
                user.CompletedMaterials.Add(material);
                await Save();
                return true;
            }
            return false;
        }

    }
}
