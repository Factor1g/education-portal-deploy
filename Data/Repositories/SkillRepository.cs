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

        public async Task<List<UserSkill>> GetUserSkills(string userId)
        {
            return await context.Set<UserSkill>()
                .Where(u => u.UserId == userId)
                .Include(us => us.Skill)
                .ToListAsync();
        }

        public async Task<bool> AcquireSkill(string userId, int skillId)
        {
            var user = await context.Set<User>().Include(u => u.Skills).FirstOrDefaultAsync(u => u.Id == userId);
            var skill = await context.Set<Skill>().FirstOrDefaultAsync(s => s.Id == skillId);

            if (user == null || skill == null)
                return false;

            var userSkill = user.Skills.FirstOrDefault(us => us.SkillId == skill.Id);
            if (userSkill == null)
            {
                user.Skills.Add(new UserSkill { SkillId = skill.Id, Skill = skill, UserId = user.Id.ToString(), User = user, Level = 1});
                System.Console.WriteLine($"New skill acquired: {skill.Name}");
            }
            else
            {
                userSkill.Level += 1;
                System.Console.WriteLine($"Skill {skill.Name} leveled up to {userSkill.Level}");
            }

            await Save();
            return true;
        }
    }
}
