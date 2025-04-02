using Model;
namespace EducationPortal.Web.Models
{
    public class SkillListViewModel
    {
        public List<Skill> Skills { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsTeacher { get; set; }

    }
}
