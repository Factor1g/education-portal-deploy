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
    }
}
