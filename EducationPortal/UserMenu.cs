using Application;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class UserMenu
    {
        private readonly IUserService _userService;
        private User _loggedInUser;
        public UserMenu(IUserService userService)
        {
            _userService = userService;

        }

        public void ShowMenu()
        {
            while (true)
            {
                System.Console.Clear();
                System.Console.WriteLine("1. Register\n2. Login\n3. Back to Main Menu");
                var choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        //Login();
                        return; 
                    case "3":
                        return; 
                    default:
                        System.Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void Register()
        {
            System.Console.Clear();
            System.Console.Write("Enter username: ");
            var username = System.Console.ReadLine();
            System.Console.Write("Enter password: ");
            var password = System.Console.ReadLine();

            //try
            //{
            //    _userService.Register(username, password);
            //    System.Console.WriteLine("Registration successful!");
            //    System.Console.ReadLine();
            //}
            //catch (InvalidOperationException ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //    System.Console.ReadLine();
            //}
        }

        //private void Login()
        //{
        //    System.Console.Clear();
        //    System.Console.Write("Enter username: ");
        //    var username = System.Console.ReadLine();
        //    System.Console.Write("Enter password: ");
        //    var password = System.Console.ReadLine();

        //    var user = _userService.Login(username, password);
        //    if (user != null)
        //    {
        //        System.Console.WriteLine("Login successful!");
        //        System.Console.ReadLine();
        //        ShowUserOptions(user);
        //    }
        //    else
        //    {
        //        System.Console.WriteLine("Invalid credentials!");
        //        System.Console.ReadLine();
        //    }
        //}

        //private void ShowUserOptions(User user)
        //{
        //    while (true)
        //    {
        //        System.Console.Clear();
        //        System.Console.WriteLine("1. View Skills\n2. Logout");               

        //        var choice = System.Console.ReadLine();
        //        switch (choice)
        //        {
        //            case "1": CreateCourse(); break;
        //            case "2": CreateMaterial(); break;
        //            case "3": ViewCourses(); break;
        //            case "4": EnrollInCourse(); break;
        //            case "5": return;
        //            default: System.Console.WriteLine("Invalid choice. Try again."); break;
        //        }
        //    }
            
        //}
        //private void CreateCourse()
        //{
        //    System.Console.Write("Enter course name: ");
        //    string name = System.Console.ReadLine();
        //    System.Console.Write("Enter course description: ");
        //    string description = System.Console.ReadLine();
        //    _userService.CreateCourse(new Course { Name = name, Description = description });
        //    System.Console.WriteLine("Course created successfully!");
        //}

        //private void CreateMaterial()
        //{
        //    System.Console.Write("Enter material title: ");
        //    string title = System.Console.ReadLine();
        //    System.Console.Write("Enter description: ");
        //    string description = System.Console.ReadLine();
        //    _userService.CreateMaterial(new Video { Title = title, Description = description }); // Defaulting to Video for now
        //    System.Console.WriteLine("Material created successfully!");
        //}

        //private void ViewCourses()
        //{
        //    System.Console.WriteLine("Courses Available:");
        //    foreach (var course in _userService.GetAllCourses())
        //    {
        //        System.Console.WriteLine($"- {course.Name}: {course.Description}");
        //    }
        //}

        //private void EnrollInCourse()
        //{
        //    System.Console.Write("Enter course name to enroll: ");
        //    string courseName = System.Console.ReadLine();
        //    var course = _userService.GetCourse(courseName);
        //    if (course != null)
        //    {
        //        _userService.EnrollInCourse(_loggedInUser, course);
        //        System.Console.WriteLine("Successfully enrolled!");
        //    }
        //    else System.Console.WriteLine("Course not found!");
        //}
    }
}
