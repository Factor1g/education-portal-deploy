using Model;
namespace EducationPortal.Web.Models
{
    public class CourseDetailViewModel
    {
        public Course Course { get; set; }
        public List<Material> Materials { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
