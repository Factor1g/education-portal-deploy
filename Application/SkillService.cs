using Model;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;

namespace Application
{
    public class SkillService : ISkillService
    {
        private ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task CreateSkill(Skill skill)
        {
            await _skillRepository.Insert(skill);
        }

        public async Task Delete(int id)
        {
            await _skillRepository.Delete(id);
        }

        public async Task<IEnumerable<Skill>> GetAllSkills()
        {
            return await _skillRepository.GetAll();
        }

        public async Task<Skill> GetById(int id)
        {
            return await _skillRepository.GetById(id);
        }

        public async Task<List<UserSkill>> GetUserSkills(string userId)
        {
            return await _skillRepository.GetUserSkills(userId);
        }

        public async Task Update(Skill skill)
        {
            await _skillRepository.Update(skill);
        }

        public async Task<bool> AcquireSkill(string userId, int skillId)
        {

            return await _skillRepository.AcquireSkill(userId, skillId);
        }
        public async Task<List<Skill>> GetSelectedSkills(List<int> selectedSkillIds)
        {
            return (await GetAllSkills()).Where(m => selectedSkillIds.Contains(m.Id)).ToList();
        }
    }
}
