using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CreatorId { get; set; }
        public User? Creator { get; set; }
        public ICollection<Material> Materials { get; set; } = new List<Material>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        public void DisplayDetails()
        {
            Console.WriteLine($"Course: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine("Materials:");
            foreach (var material in Materials)
            {
                material.DisplayDetails();
            }
            Console.WriteLine("Skills Acquired:");
            foreach (var skill in Skills)
            {
                skill.DisplayDetails();
            }
        }
    }
}
