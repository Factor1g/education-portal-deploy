using Application;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class Menu : IMenu
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly IMaterialService _materialService;
        private readonly ISkillService _skillService;
        private User _loggedInUser;

        public Menu(IUserService userService, ICourseService courseService, IMaterialService materialService, ISkillService skillService)
        {
            _userService = userService;
            _courseService = courseService;
            _materialService = materialService;
            _skillService = skillService;
            System.Console.WriteLine("Menu set up");
        }

        public async Task Start()
        {
            System.Console.Clear();
            System.Console.WriteLine("Welcome to the Education Portal!");
            System.Console.WriteLine("1. Login\n2. Register");
            string choice = System.Console.ReadLine();
            _loggedInUser = null;

            //while (_loggedInUser == null)
            //{
            //    if (choice == "1")
            //    {
            //        System.Console.Write("Enter username: ");
            //        string username = System.Console.ReadLine();
            //        System.Console.Write("Enter password: ");
            //        string password = System.Console.ReadLine();
            //        try
            //        {
            //            System.Console.WriteLine("Fetching data...");
            //            _loggedInUser = _userService.Login(username, password);
            //        }
            //        catch (Exception e)
            //        {
            //            System.Console.WriteLine(e.Message);
            //        }
            //    }
            //    else if (choice == "2")
            //    {
            //        System.Console.Write("Enter new username: ");
            //        string username = System.Console.ReadLine();
            //        System.Console.Write("Enter new password: ");
            //        string password = System.Console.ReadLine();
            //        try
            //        {
            //            _userService.Register(username, password);
            //        }
            //        catch (Exception e)
            //        {
            //            System.Console.WriteLine(e.Message);
            //        }
            //        System.Console.WriteLine("Registration successful! Please log in.");
            //        choice = "1";
            //    }
            //    else
            //    {
            //        System.Console.WriteLine("Invalid choice. Please select 1 or 2.");
            //        choice = System.Console.ReadLine();
            //    }
            //}

            while (true)
            {                
                System.Console.Clear();
                System.Console.WriteLine("1. Create Course\n2. Update Course\n3. Delete Course\n4. View Courses\n5. Create Material\n6. Update Material\n7. Delete Material\n8. View Material\n9. Enroll in Course\n10. View all Materials\n11. Complete Material\n12. User Profile\n13. Create Skill\n14. View Skills\n15. Update Skill\n16. Delete Skill\n17. Logout\n0. Exit");
                var choice2 = System.Console.ReadLine();
                switch (choice2)
                {
                    case "1": await CreateCourse(); break;
                    case "2": await UpdateCourse(); break;
                    case "3": DeleteCourse(); break;
                    case "4": await ViewCourses(); break;
                    case "5": CreateMaterial(); break;
                    case "6": await UpdateMaterial(); break;
                    case "7": DeleteMaterial(); break;
                    case "8": await ViewMaterial(); break;
                    case "9": await EnrollInCourse(); break;
                    case "10": await GetAllMaterials(); break;
                    case "11": await CompleteMaterial(); break;
                    case "12": await ViewProfile(_loggedInUser); break;
                    case "13": await CreateSkill(); break;
                    case "14": await ViewSkills(); break;
                    case "15": await UpdateSkill(); break;
                    case "16": DeleteSkill(); break;
                    case "17": Logout(); ; break;
                    
                    case "0": return;
                    default: System.Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }

        public void Logout()
        {
            Start();
        }

        public async Task CreateCourse()
        {
            System.Console.Write("Enter course name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter course description: ");
            string description = System.Console.ReadLine();

            List<Material> materials = new List<Material>();
            while (true)
            {
                System.Console.Write("Enter material ID to add (or type 'done' to finish): ");
                string input = System.Console.ReadLine();
                if (input.ToLower() == "done")
                    break;

                if (int.TryParse(input, out int materialId))
                {
                    var material = await _materialService.GetById(materialId);
                    if (material != null)
                        materials.Add(material);
                    else
                        System.Console.WriteLine("Material not found.");
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid material ID.");
                }
            }

            List<Skill> skills = new List<Skill>();
            while (true)
            {
                System.Console.Write("Enter skill ID to add (or type 'done' to finish): ");
                string input = System.Console.ReadLine()?.Trim().ToLower();
                if (input == "done")
                    break;

                if (int.TryParse(input, out int skillId))
                {
                    var skill = await _skillService.GetById(skillId);
                    if (skill != null)
                        skills.Add(skill);
                    else
                        System.Console.WriteLine("Skill not found.");
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid skill ID.");
                }
            }

            var course = new Course { Name = name, Description = description, Materials = materials, Skills = skills };
            _courseService.CreateCourse(course);
            System.Console.WriteLine("Course created successfully!");
            System.Console.ReadLine();
        }

        public async Task UpdateCourse()
        {
            System.Console.Write("Enter course ID to update: ");
            if (!int.TryParse(System.Console.ReadLine(), out int id))
            {
                System.Console.WriteLine("Invalid course ID.");
                return;
            }

            var course = await _courseService.GetById(id);
            if (course == null)
            {
                System.Console.WriteLine("Course not found!");
                return;
            }

            System.Console.WriteLine($"Current name: {course.Name}");
            System.Console.Write("Enter new name (or press Enter to keep current): ");
            string nameInput = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nameInput))
                course.Name = nameInput;

            System.Console.WriteLine($"Current description: {course.Description}");
            System.Console.Write("Enter new description (or press Enter to keep current): ");
            string descriptionInput = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descriptionInput))
                course.Description = descriptionInput;            

            while (true)
            {
                System.Console.Write("Enter material ID to add/remove (or type 'done' to finish, or 'skip' to keep current materials): ");
                string input = System.Console.ReadLine()?.Trim().ToLower();
                if (input == "done" || input == "skip")
                    break;

                if (int.TryParse(input, out int materialId))
                {
                    await _courseService.AddMaterialToCourse(id, materialId);
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid material ID.");
                }
            }


            while (true)
            {
                System.Console.Write("Enter skill ID to add/remove (or type 'done' to finish, or 'skip' to keep current skills): ");
                string input = System.Console.ReadLine()?.Trim().ToLower();
                if (input == "done" || input == "skip")
                    break;

                if (int.TryParse(input, out int skillId))
                {
                    await _courseService.AddSkillToCourse(id, skillId);
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid skill ID.");
                }
            }
            
            System.Console.WriteLine("Course updated successfully!");
            System.Console.ReadLine();
        }

        public void DeleteCourse()
        {
            System.Console.Write("Enter course ID to delete: ");
            int id = int.Parse(System.Console.ReadLine());
            _courseService.Delete(id);
            System.Console.WriteLine("Course deleted successfully!");
            System.Console.ReadLine();
        }

        public async Task ViewCourses()
        {
            System.Console.Clear();
            System.Console.WriteLine("Courses Available:");

            var courses = await _courseService.GetAllCourses();

            if (!courses.Any())
            {
                System.Console.WriteLine("No courses available.");
                return;
            }

            foreach (var course in courses)
            {
                System.Console.WriteLine($"- ID: {course.Id} {course.Name}: {course.Description}");

                if (course.Materials.Any())
                {
                    System.Console.WriteLine("  Materials:");
                    foreach (var material in course.Materials)
                    {
                        System.Console.WriteLine($"    - {material.Title} ({material.GetType().Name})");
                    }                                        
                }
                else
                {
                    System.Console.WriteLine("  No materials available.");
                }
                if (course.Skills.Any())
                {
                    System.Console.WriteLine("  Acquirable skills:");
                    foreach (var skill in course.Skills)
                    {
                        System.Console.WriteLine($"    - {skill.Name}");
                    }
                }
                else
                {
                    System.Console.WriteLine("  No skills available.");
                }
            }
            System.Console.ReadKey();
        }

        public void CreateMaterial()
        {
            System.Console.Write("Enter material type (Article, Video, Book): ");
            string materialType = System.Console.ReadLine().ToLower();
            System.Console.Write("Enter material title: ");
            string title = System.Console.ReadLine();
            System.Console.Write("Enter description: ");
            string description = System.Console.ReadLine();

            Material material = null;

            if (materialType == "article")
            {

                System.Console.Write("Enter publication date (YYYY-MM-DD): ");
                DateTime publicationDate = DateTime.Parse(System.Console.ReadLine());
                System.Console.Write("Enter resource URL: ");
                string resource = System.Console.ReadLine();
                material = new Article { Title = title, Description = description, PublicationDate = publicationDate, Resource = resource };
            }
            else if (materialType == "video")
            {
                System.Console.Write("Enter duration (in minutes): ");
                int duration = int.Parse(System.Console.ReadLine());
                System.Console.Write("Enter quality (e.g., HD, 4K): ");
                string quality = System.Console.ReadLine();
                material = new Video { Title = title, Description = description, Duration = duration, Quality = quality };
            }
            else if (materialType == "book")
            {
                System.Console.Write("Enter author name: ");
                string author = System.Console.ReadLine();
                System.Console.Write("Enter number of pages: ");
                int pages = int.Parse(System.Console.ReadLine());
                System.Console.Write("Enter format (e.g., PDF, EPUB): ");
                string format = System.Console.ReadLine();
                System.Console.Write("Enter publication year: ");
                int year = int.Parse(System.Console.ReadLine());
                material = new Book { Title = title, Description = description, Author = author, Pages = pages, Format = format, Year = year };
            }
            else
            {
                System.Console.WriteLine("Invalid material type entered.");
                return;
            }

            _materialService.CreateMaterial(material);
            System.Console.WriteLine("Material created successfully!");
            System.Console.ReadLine();
        }

        public async Task UpdateMaterial()
        {
            System.Console.Write("Enter material ID to update: ");
            int id = int.Parse(System.Console.ReadLine());
            var material = await _materialService.GetById(id);
            if (material != null)
            {
                System.Console.Write("Enter new description: ");
                material.Description = System.Console.ReadLine();
                _materialService.UpdateMaterial(material);
                System.Console.WriteLine("Material updated successfully!");
                System.Console.ReadLine();
            }
            else System.Console.WriteLine("Material not found!");
            System.Console.ReadLine();
        }

        public void DeleteMaterial()
        {
            System.Console.Write("Enter material ID to delete: ");
            int id = int.Parse(System.Console.ReadLine());
            _materialService.Delete(id);
            System.Console.WriteLine("Material deleted successfully!");
            System.Console.ReadLine();
        }

        public async Task ViewMaterial()
        {
            System.Console.Write("Enter material ID to view: ");
            int id = int.Parse(System.Console.ReadLine());
            var material = await _materialService.GetById(id);
            if (material != null)
            {
                System.Console.WriteLine($"Title: {material.Title}, Description: {material.Description}");
            }
            else
            {
                System.Console.WriteLine("Material not found!");
            }
            System.Console.ReadLine();
        }

        public async Task ViewAllMaterials()
        {
            System.Console.WriteLine("All Materials:");
            
            foreach (var material in await _materialService.GetAllMaterials())
            {
                System.Console.WriteLine($"ID:{material.Id} Title: {material.Title}, Description: {material.Description}");
                if (material is Article article)
                {
                    System.Console.WriteLine($"Publication Date: {article.PublicationDate}, Resource: {article.Resource}");
                }
                else if (material is Video video)
                {
                    System.Console.WriteLine($"Duration: {video.Duration} mins, Quality: {video.Quality}");
                }
                else if (material is Book book)
                {
                    System.Console.WriteLine($"Author: {book.Author}, Pages: {book.Pages}, Format: {book.Format}, Year: {book.Year}");
                }
                System.Console.WriteLine("---------------------------");
            }
            System.Console.ReadLine();
        }

        public async Task EnrollInCourse()
        {
            System.Console.Write("Enter course ID to enroll: ");
            int id = int.Parse(System.Console.ReadLine());
            try
            {
                await _courseService.EnrollInCourse(_loggedInUser.Id, id);
                System.Console.WriteLine("Successfully enrolled!");
                System.Console.ReadLine();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            System.Console.ReadLine();
        }
        public async Task CreateSkill()
        {
            System.Console.Write("Enter skill name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter skill description: ");
            string description = System.Console.ReadLine();

            Skill skill = new Skill { Name = name, Description = description };
            _skillService.CreateSkill(skill);
            System.Console.WriteLine("Skill created successfully!");
            System.Console.ReadLine();
        }

        public async Task ViewSkills()
        {
            System.Console.WriteLine("Skills Available:");
            var skills = await _skillService.GetAllSkills();

            if (!skills.Any())
            {
                System.Console.WriteLine("No skills available.");
                return;
            }

            foreach (var skill in skills)
            {
                System.Console.WriteLine($"- ID: {skill.Id}, Name: {skill.Name}, Description: {skill.Description}");
            }
            System.Console.ReadLine();
        }

        public async Task UpdateSkill()
        {
            System.Console.Write("Enter skill ID to update: ");
            if (!int.TryParse(System.Console.ReadLine(), out int id))
            {
                System.Console.WriteLine("Invalid skill ID.");
                return;
            }

            var skill = await _skillService.GetById(id);
            if (skill == null)
            {
                System.Console.WriteLine("Skill not found!");
                return;
            }

            System.Console.WriteLine($"Current name: {skill.Name}");
            System.Console.Write("Enter new name (or press Enter to keep current): ");
            string nameInput = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nameInput))
                skill.Name = nameInput;

            System.Console.WriteLine($"Current description: {skill.Description}");
            System.Console.Write("Enter new description (or press Enter to keep current): ");
            string descriptionInput = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(descriptionInput))
                skill.Description = descriptionInput;

            await _skillService.Update(skill);
            System.Console.WriteLine("Skill updated successfully!");
            System.Console.ReadLine();
        }

        public void DeleteSkill()
        {
            System.Console.Write("Enter skill ID to delete: ");
            if (!int.TryParse(System.Console.ReadLine(), out int id))
            {
                System.Console.WriteLine("Invalid skill ID.");
                return;
            }
            _skillService.Delete(id);
            System.Console.WriteLine("Skill deleted successfully!");
            System.Console.ReadLine();
        }

        public async Task ViewProfile(User user)
        {
            if (user == null)
            {
                System.Console.WriteLine("User not found!");
                return;
            }

            System.Console.Clear();
            System.Console.WriteLine($"User Profile: {user.UserName}\n");

            System.Console.WriteLine("Enrolled Courses:");
            foreach (var course in await _courseService.GetInProgressCourses(user.Id))
            {
                System.Console.WriteLine($"- {course.Name}");
            }

            System.Console.WriteLine("\nCompleted Courses:");
            foreach (var course in await _courseService.GetCompletedCourses(user.Id))
            {
                System.Console.WriteLine($"- {course.Name}");
            }

            System.Console.WriteLine("\nSkills:");
            foreach (var skill in await _skillService.GetUserSkills(user.Id))
            {
                System.Console.WriteLine($"- {skill.Skill.Name} (Level {skill.Level})");
            }

            System.Console.WriteLine("\nCompleted Materials:");
            foreach (var material in await _materialService.GetCompletedMaterials(user.Id))
            {
                System.Console.WriteLine($"- {material.Title} ({material.GetType().Name})");
            }

            System.Console.WriteLine("\nCourse Completion Progress:");
            foreach (var course in await _courseService.GetInProgressCourses(user.Id))
            {
                System.Console.WriteLine($"- {course.Name}");
            }
            System.Console.ReadKey();
        }

        public async Task CompleteMaterial()
        {
            System.Console.Write("Enter material ID to complete: ");
            int id = int.Parse(System.Console.ReadLine());
            var material = await _materialService.GetById(id);
            try
            {
                await _materialService.CompleteMaterial(_loggedInUser.Id, id);
                System.Console.WriteLine($"You successfully finished {material.Title} learning material!");
                System.Console.ReadLine();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            System.Console.ReadLine();
        }

        public async Task GetAllMaterials()
        {
            var materials = await _materialService.GetAllMaterials();

            if (!materials.Any())
            {
                System.Console.WriteLine("No materials available.");
                return;
            }

            System.Console.WriteLine("List of Materials:");
            foreach (var material in materials)
            {
                System.Console.WriteLine($"- ID: {material.Id}, Title: {material.Title}, Type: {material.GetType().Name}");
            }

            System.Console.ReadLine();
        }
    }
}
