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
        void CreateSkill(Skill course);
        Task Update(Skill course);
        void Delete(int id);
        Task<List<UserSkill>> GetUserSkills(int userId);
    }
}
