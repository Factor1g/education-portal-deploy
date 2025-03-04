using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<UserSkill>> GetUserSkills(int userId);
    }

}
