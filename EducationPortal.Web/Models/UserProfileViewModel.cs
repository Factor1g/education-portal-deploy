using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace EducationPortal.Web.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public IEnumerable<Course> EnrolledCourses { get; set; }
        public IEnumerable<Course> CompletedCourses { get; set; }
        public IEnumerable<UserSkill> Skills { get; set; }
        public IEnumerable<Material> CompletedMaterials { get; set; }
    }
}

