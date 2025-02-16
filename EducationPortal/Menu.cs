using Application;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class Menu
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        private readonly IMaterialService _materialService;
        private User _loggedInUser;

        public Menu(IUserService userService)
        {
            _userService = userService;
            
        }

        public void Start()
        {
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
                    _loggedInUser = _userService.Login(username, password);
                    if (_loggedInUser == null)
                        System.Console.WriteLine("Invalid credentials. Try again.");
                }
                else if (choice == "2")
                {
                    System.Console.Write("Enter new username: ");
                    string username = System.Console.ReadLine();
                    System.Console.Write("Enter new password: ");
                    string password = System.Console.ReadLine();
                    _userService.Register(username, password);
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
                System.Console.WriteLine("1. Create Course\n2. Update Course\n3. Delete Course\n4. View Courses\n5. Create Material\n6. Update Material\n7. Delete Material\n8. View Material\n9. Enroll in Course\n10. Exit");
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
                    case "10": return;
                    default: System.Console.WriteLine("Invalid choice. Try again."); break;
                }
            }
        }

        private void CreateCourse()
        {
            System.Console.Write("Enter course name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter course description: ");
            string description = System.Console.ReadLine();
            _courseService.CreateCourse(new Course { Name = name, Description = description });
            System.Console.WriteLine("Course created successfully!");
            System.Console.ReadLine();
        }

        private void UpdateCourse()
        {
            System.Console.Write("Enter course name to update: ");
            string name = System.Console.ReadLine();
            var course = _courseService.GetCourse(name);
            if (course != null)
            {
                System.Console.Write("Enter new description: ");
                course.Description = System.Console.ReadLine();
                _courseService.UpdateCourse(course);
                System.Console.WriteLine("Course updated successfully!");
                System.Console.ReadLine();
            }
            else System.Console.WriteLine("Course not found!");
            System.Console.ReadLine();
        }

        private void DeleteCourse()
        {
            System.Console.Write("Enter course name to delete: ");
            string name = System.Console.ReadLine();
            _courseService.DeleteCourse(name);
            System.Console.WriteLine("Course deleted successfully!");
            System.Console.ReadLine();
        }

        private void ViewCourses()
        {
            System.Console.WriteLine("Courses Available:");
            foreach (var course in _courseService.GetAllCourses())
            {
                System.Console.WriteLine($"- {course.Name}: {course.Description}");
            }
            System.Console.ReadLine();
        }

        private void CreateMaterial()
        {
            System.Console.Write("Enter material title: ");
            string title = System.Console.ReadLine();
            System.Console.Write("Enter description: ");
            string description = System.Console.ReadLine();
            _materialService.CreateMaterial(new Video { Title = title, Description = description }); // Defaulting to Video for now
            System.Console.WriteLine("Material created successfully!");
            System.Console.ReadLine();
        }

        private void UpdateMaterial()
        {
            System.Console.Write("Enter material title to update: ");
            string title = System.Console.ReadLine();
            var material = _materialService.GetMaterial(title);
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

        private void DeleteMaterial()
        {
            System.Console.Write("Enter material title to delete: ");
            string title = System.Console.ReadLine();
            _materialService.DeleteMaterial(title);
            System.Console.WriteLine("Material deleted successfully!");
            System.Console.ReadLine();
        }

        private void ViewMaterial()
        {
            System.Console.Write("Enter material title to view: ");
            string title = System.Console.ReadLine();
            var material = _materialService.GetMaterial(title);
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

        private void EnrollInCourse()
        {
            System.Console.Write("Enter course name to enroll: ");
            string courseName = System.Console.ReadLine();
            var course = _courseService.GetCourse(courseName);
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
