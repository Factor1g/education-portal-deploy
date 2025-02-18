using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }        

        public void DisplayDetails()
        {
            Console.WriteLine($"Skill: {Name}, Description: {Description}");
        }
    }
}
