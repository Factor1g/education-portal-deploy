
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : IdentityUser
    {
        public ICollection<Course> CompletedCourses { get; set; } = new List<Course>();
        public ICollection<Course> InProgressCourses { get; set; } = new List<Course>();
        public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
        public ICollection<Material> CompletedMaterials { get; set; }

        public string Role { get; set; } = "Student";
    }
}
