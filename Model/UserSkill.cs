using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserSkill
    {
        public Skill Skill { get; set; }
        public int Level { get; set; } = 1;

        public void DisplayDetails()
        {
            Console.WriteLine($"Skill: {Skill.Name}, Level: {Level}, Description: {Skill.Description}");
        }
    }
}
