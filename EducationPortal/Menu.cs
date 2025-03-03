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
        private User _loggedInUser;

        public Menu(IUserService userService, ICourseService courseService, IMaterialService materialService)
        {
            _userService = userService;
            _courseService = courseService;
            _materialService = materialService;
            System.Console.WriteLine("Menu set up");
        }

        public void Start()
        {
            System.Console.Clear();
            System.Console.WriteLine("Welcome to the Education Portal!");
            System.Console.WriteLine("1. Login\n2. Register");
            string choice = System.Console.ReadLine();
            _loggedInUser = null;

            while (_loggedInUser == null)
            {
                if (choice == "1")
                {
                    System.Console.Write("Enter username: ");
                    string username = System.Console.ReadLine();
                    System.Console.Write("Enter password: ");
                    string password = System.Console.ReadLine();
                    try
                    {
                        System.Console.WriteLine("Fetching data...");
                        _loggedInUser = _userService.Login(username, password);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
                else if (choice == "2")
                {
                    System.Console.Write("Enter new username: ");
                    string username = System.Console.ReadLine();
                    System.Console.Write("Enter new password: ");
                    string password = System.Console.ReadLine();
                    try
                    {
                        _userService.Register(username, password);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                    System.Console.WriteLine("Registration successful! Please log in.");
                    choice = "1";
                }
                else
                {
                    System.Console.WriteLine("Invalid choice. Please select 1 or 2.");
                    choice = System.Console.ReadLine();
                }
            }

            while (true)
            {                
                System.Console.Clear();
                System.Console.WriteLine("1. Create Course\n2. Update Course\n3. Delete Course\n4. View Courses\n5. Create Material\n6. Update Material\n7. Delete Material\n8. View Material\n9. Enroll in Course\n10. Logout\n0. Exit");
                var choice2 = System.Console.ReadLine();
                switch (choice2)
                {
                    case "1": CreateCourse(); break;
                    case "2": UpdateCourse(); break;
                    case "3": DeleteCourse(); break;
                    case "4": ViewCourses(); break;
                    case "5": CreateMaterial(); break;
                    case "6": UpdateMaterial(); break;
                    case "7": DeleteMaterial(); break;
                    case "8": ViewMaterial(); break;
                    case "9": EnrollInCourse(); break;
                    case "11": ViewAllMaterials(); break;
                    case "10": Logout(); return;
                    case "0": return;
                    default: System.Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }

        public void Logout()
        {
            Start();
        }

        public async void CreateCourse()
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
                    var material = await _materialService.GetMaterial(materialId);
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

            var course = new Course { Name = name, Description = description, Materials = materials };
            _courseService.CreateCourse(course);
            System.Console.WriteLine("Course created successfully!");
            System.Console.ReadLine();
        }

        public async void UpdateCourse()
        {
            System.Console.Write("Enter course ID to update: ");
            int id = int.Parse(System.Console.ReadLine());
            var course = await _courseService.GetById(id);
            if (course != null)
            {
                System.Console.Write("Enter new description: ");
                course.Description = System.Console.ReadLine();
                _courseService.Update(course);
                System.Console.WriteLine("Course updated successfully!");
                System.Console.ReadLine();
            }
            else System.Console.WriteLine("Course not found!");
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

        public async void ViewCourses()
        {
            System.Console.WriteLine("Courses Available:");
            foreach (var course in await _courseService.GetAllCourses())
            {
                System.Console.WriteLine($"- {course.Name}: {course.Description}");
                if (course.Materials != null && course.Materials.Any())
                {
                    System.Console.WriteLine("  Materials:");
                    foreach (var material in course.Materials)
                    {
                        System.Console.WriteLine($"    - {material.Title} ({material.GetType().Name})");
                    }
                }
                else
                {
                    System.Console.WriteLine("No materials available.");
                }
            }
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

        public async void UpdateMaterial()
        {
            System.Console.Write("Enter material ID to update: ");
            int id = int.Parse(System.Console.ReadLine());
            var material = await _materialService.GetMaterial(id);
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
            _materialService.DeleteMaterial(id);
            System.Console.WriteLine("Material deleted successfully!");
            System.Console.ReadLine();
        }

        public async void ViewMaterial()
        {
            System.Console.Write("Enter material ID to view: ");
            int id = int.Parse(System.Console.ReadLine());
            var material = await _materialService.GetMaterial(id);
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

        public void ViewAllMaterials()
        {
            System.Console.WriteLine("All Materials:");
            foreach (var material in _materialService.GetAllMaterials())
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

        public async void EnrollInCourse()
        {
            System.Console.Write("Enter course ID to enroll: ");
            int id = int.Parse(System.Console.ReadLine());
            var course = await _courseService.GetById(id);
            if (course != null)
            {
                _userService.EnrollInCourse(_loggedInUser, course);
                System.Console.WriteLine("Successfully enrolled!");
                System.Console.ReadLine();
            }
            else System.Console.WriteLine("Course not found!");
            System.Console.ReadLine();
        }
    }
}
