﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Course> CompletedCourses { get; set; } = new List<Course>();
        public List<Course> InProgressCourses { get; set; } = new List<Course>();
        public List<UserSkill> Skills { get; set; } = new List<UserSkill>();
        public List<Material> CompletedMaterials { get; set; }
        public void DisplaySkills()
        {
            Console.WriteLine("Your Skills:");
            foreach (var skill in Skills)
            {
                skill.DisplayDetails();
            }
        }

        public void AddSkill(Skill skill)
        {
            var existingSkill = Skills.FirstOrDefault(s => s.Skill.Name == skill.Name);
            if (existingSkill != null)
            {
                existingSkill.Level++;
            }
            else
            {
                Skills.Add(new UserSkill { Skill = skill, Level = 1 });
            }
        }

        public double GetCousreProgress(Course course)
        {
            if (!InProgressCourses.Contains(course))
            {
                return 0;
            }
            return (double)course.Materials.Count(m => CompletedMaterials.Any(cm => cm.Title == m.Title)) /course.Materials.Count * 100;
        }
    }
}
