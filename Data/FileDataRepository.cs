using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FileDataRepository : IDataRepository
    {
        private static string UsersFile = "users.txt";
        private static string MaterialsFile = "materials.txt";
        private static string CoursesFile = "courses.txt";
        private static string SkillsFile = "skills.txt";

        public void AddCourse(Course course)
        {
            var courses = GetAllCourses();
            courses.Add(course);
            SaveCourses(courses);
        }

        public void AddMaterial(Material material)
        {
            var materials = GetAllMaterials();
            materials.Add(material);
            SaveMaterials(materials);
        }

        public void AddSkill(Skill skill)
        {
            var skills = GetAllSkills();
            skills.Add(skill);
            SaveSkills(skills);
        }

        public void AddUser(User user)
        {
            var users = GetAllUsers();
            users.Add(user);
            SaveUsers(users);
        }

        public void DeleteCourse(string name)
        {
            var courses = GetAllCourses();
            var course = courses.FirstOrDefault(c => c.Name == name);
            if (course != null)
            {
                courses.Remove(course);
                SaveCourses(courses);
            }
        }

        public void DeleteMaterial(string title)
        {
            var materials = GetAllMaterials();
            var material = materials.FirstOrDefault(m => m.Title == title);
            if (material != null)
            {
                materials.Remove(material);
                SaveMaterials(materials);
            }
        }

        public void DeleteSkill(string name)
        {
            var skills = GetAllSkills();
            var skill = skills.FirstOrDefault(s => s.Name == name);
            if (skill != null)
            {
                skills.Remove(skill);
                SaveSkills(skills);
            }
        }

        public void DeleteUser(string username)
        {
            var users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                users.Remove(user);
                SaveUsers(users);
            }
        }

        public List<Course> GetAllCourses()
        {
            return LoadCourses();
        }

        public List<Material> GetAllMaterials()
        {
            return LoadMaterials();
        }

        public List<Skill> GetAllSkills()
        {
            return LoadSkills();
        }

        public List<User> GetAllUsers()
        {
            return LoadUsers();
        }

        public Course GetCourse(string name)
        {
            return GetAllCourses().FirstOrDefault(c => c.Name == name);
        }

        public Material GetMaterial(string title)
        {
            return GetAllMaterials().FirstOrDefault(m => m.Title == title);
        }

        public Skill GetSkill(string name)
        {
            return GetAllSkills().FirstOrDefault(s =>s.Name == name);
        }

        public User GetUser(string username)
        {
            return GetAllUsers().FirstOrDefault(u => u.Username == username);
        }

        public List<Course> LoadCourses()
        {
            List<Course> courses = new List<Course>();
            if (File.Exists(CoursesFile))
            {
                var lines = File.ReadAllLines(CoursesFile);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    var course = new Course
                    {
                        Name = parts[0],
                        Description = parts[1]
                    };
                    
                    if (parts.Length > 2 && !string.IsNullOrEmpty(parts[2]))
                    {
                        var materialTitles = parts[2].Split(',');
                        foreach (var title in materialTitles)
                        {
                            var material = LoadMaterials().FirstOrDefault(m => m.Title == title);
                            if (material != null)
                            {
                                course.Materials.Add(material);
                            }
                        }
                    }
                    
                    if (parts.Length > 3 && !string.IsNullOrEmpty(parts[3]))
                    {
                        var skillNames = parts[3].Split(',');
                        foreach (var name in skillNames)
                        {
                            var skill = new Skill { Name = name };
                            course.Skills.Add(skill);
                        }
                    }
                    courses.Add(course);
                }
            }
            return courses;
        }

        public List<Material> LoadMaterials()
        {
            List<Material> materials = new List<Material>();
            if (File.Exists(MaterialsFile))
            {
                var lines = File.ReadAllLines(MaterialsFile);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts[0] == "Video")
                    {
                        materials.Add(new Video
                        {
                            Title = parts[1],
                            Description = parts[2],
                            Duration = int.Parse(parts[3]),
                            Quality = parts[4]
                        });
                    }
                    else if (parts[0] == "Book")
                    {
                        materials.Add(new Book
                        {
                            Title = parts[1],
                            Description = parts[2],
                            Author = parts[3],
                            Pages = int.Parse(parts[4]),
                            Format = parts[5],
                            Year = int.Parse(parts[6])
                        });
                    }
                    else if (parts[0] == "Article")
                    {
                        materials.Add(new Article
                        {
                            Title = parts[1],
                            Description = parts[2],
                            PublicationDate = DateTime.Parse(parts[3]),
                            Resource = parts[4]
                        });
                    }
                }
            }
            return materials;
        }

        public List<User> LoadUsers()
        {
            List<User> users = new List<User>();
            if (File.Exists(UsersFile))
            {
                var lines = File.ReadAllLines(UsersFile);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    var user = new User
                    {
                        Username = parts[0],
                        Password = parts[1]
                    };
                    
                    if (parts.Length > 2 && !string.IsNullOrEmpty(parts[2]))
                    {
                        var completedCourseNames = parts[2].Split(',');
                        foreach (var courseName in completedCourseNames)
                        {
                            var course = LoadCourses().FirstOrDefault(c => c.Name == courseName);
                            if (course != null)
                            {
                                user.CompletedCourses.Add(course);
                            }
                        }
                    }
                    
                    if (parts.Length > 3 && !string.IsNullOrEmpty(parts[3]))
                    {
                        var inProgressCourseNames = parts[3].Split(',');
                        foreach (var courseName in inProgressCourseNames)
                        {
                            var course = LoadCourses().FirstOrDefault(c => c.Name == courseName);
                            if (course != null)
                            {
                                user.InProgressCourses.Add(course);
                            }
                        }
                    }
                    
                    if (parts.Length > 4 && !string.IsNullOrEmpty(parts[4]))
                    {
                        var skillEntries = parts[4].Split(';');
                        foreach (var skillEntry in skillEntries)
                        {
                            var skillParts = skillEntry.Split(':');
                            if (skillParts.Length == 3)
                            {
                                var skill = new Skill
                                {
                                    Name = skillParts[0],
                                    Description = skillParts[1]
                                };
                                user.Skills.Add(new UserSkill
                                {
                                    Skill = skill,
                                    Level = int.Parse(skillParts[2])
                                });
                            }
                        }
                    }
                    users.Add(user);
                }
            }
            return users;
        }

        public List<Skill> LoadSkills()
        {
           List<Skill> skills = new List<Skill>();
            var lines = File.ReadAllLines(SkillsFile);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                skills.Add(new Skill { Name = parts[0], Description = parts[1] });
            }
            return skills;
        }

        public void SaveCourses(List<Course> courses)
        {
            using (StreamWriter writer = new StreamWriter(CoursesFile))
            {
                foreach (var course in courses)
                {
                    var materialTitles = course.Materials != null && course.Materials.Any()
                    ? string.Join(",", course.Materials.Select(m => m.Title))
                    : "";

                    var skillNames = course.Skills != null && course.Skills.Any()
                    ? string.Join(",", course.Skills.Select(s => s.Name))
                    : "";

                    writer.WriteLine($"{course.Name}|{course.Description}|{materialTitles}|{skillNames}");
                }
            }
        }

        public void SaveMaterials(List<Material> materials)
        {
            using (StreamWriter writer = new StreamWriter(MaterialsFile))
            {
                foreach (var material in materials)
                {
                    if (material is Video video)
                    {
                        writer.WriteLine($"Video|{video.Title}|{video.Description}|{video.Duration}|{video.Quality}");
                    }
                    else if (material is Book book)
                    {
                        writer.WriteLine($"Book|{book.Title}|{book.Description}|{book.Author}|{book.Pages}|{book.Format}|{book.Year}");
                    }
                    else if (material is Article article)
                    {
                        writer.WriteLine($"Article|{article.Title}|{article.Description}|{article.PublicationDate}|{article.Resource}");
                    }
                }
            }
        }

        public void SaveUsers(List<User> users)
        {
            using (StreamWriter writer = new StreamWriter(UsersFile))
            {
                foreach (var user in users)
                {
                    var completedCourses = string.Join(",", user.CompletedCourses.Select(c => c.Name));
                    var inProgressCourses = string.Join(",", user.InProgressCourses.Select(c => c.Name));
                    var skills = string.Join(";", user.Skills.Select(s => $"{s.Skill.Name}:{s.Skill.Description}:{s.Level}"));

                    writer.WriteLine($"{user.Username}|{user.Password}|{completedCourses}|{inProgressCourses}|{skills}");
                }
            }
        }

        public void SaveSkills(List<Skill> skills)
        {
            using (StreamWriter writer = new StreamWriter(SkillsFile))
            {
                foreach (var skill in skills)
                {
                    writer.WriteLine($"{skill.Name}|{skill.Description}");
                }
            }                
        }
        public void UpdateCourse(Course course)
        {
            var courses = GetAllCourses();
            var existingCourse = courses.FirstOrDefault(c => c.Name == course.Name);
            if (existingCourse != null)
            {
                courses.Remove(existingCourse);
                courses.Add(course);
                SaveCourses(courses);
            }
        }

        public void UpdateMaterial(Material material)
        {
            var materials = GetAllMaterials();
            var existingMaterial = materials.FirstOrDefault(m => m.Title == material.Title);
            if (existingMaterial != null)
            {
                materials.Remove(existingMaterial);
                materials.Add(material);
                SaveMaterials(materials);
            }
        }

        public void UpdateSkill(Skill skill)
        {
            var skills = GetAllSkills();
            var existingSkill = skills.FirstOrDefault(s => s.Name == skill.Name);
            if (existingSkill != null)
            {
                skills.Remove(existingSkill);
                skills.Add(skill);
                SaveSkills(skills);
            }
        }

        public void UpdateUser(User user)
        {
            var users = GetAllUsers();
            var existingUser = users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                users.Remove(existingUser);
                users.Add(user);
                SaveUsers(users);
            }
        }
    }
}
