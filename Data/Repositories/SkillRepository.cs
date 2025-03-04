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
    public class SkillRepository : EfBaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(EducationPortalContext context) : base(context)
        {
        }

        public async Task<List<UserSkill>> GetUserSkills(int userId)
        {
            return await context.Set<UserSkill>()
                .Where(u => u.UserId == userId)                
                .ToListAsync();
        }
    }
}
