using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> GetAllSkills();
        Task<Skill> GetById(int id);
        Task CreateSkill(Skill course);
        Task Update(Skill course);
        Task Delete(int id);
        Task<List<UserSkill>> GetUserSkills(string userId);
        Task<bool> AcquireSkill(string userId, int skillId);
        Task<List<Skill>> GetSelectedSkills(List<int> selectedSkillIds);
    }
}
