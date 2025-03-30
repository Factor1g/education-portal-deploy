using Model;
using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Course>? Courses { get; set; }
        public string? CurrentUserId { get; set; }
        public bool IsTeacher { get; set; }
        public List<int>? SubscribedCourseIds { get; set; }

        // Optional future support for material or skill selection
        public List<int> SelectedMaterialIds { get; set; } = new();
        public List<int> SelectedSkillIds { get; set; } = new();

        public IEnumerable<Material>? AvailableMaterials { get; set; }
        public IEnumerable<Skill>? AvailableSkills { get; set; }        
    }

}
