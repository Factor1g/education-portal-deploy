using Model;

namespace EducationPortal.Web.Models
{
    public class MaterialListViewModel
    {
        public List<Material> Materials { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsTeacher { get; set; }

    }
}
