using Application;
using Console;
using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EducationPortal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IDataRepository dataRepository = new FileDataRepository();
            //IUserService userService = new UserService(dataRepository);

            //var userMenu = new UserMenu(userService);

            //while (true)
            //{
            //    System.Console.Clear();
            //    System.Console.WriteLine("1. User Menu\n2. Course Menu\n3. Material Menu\n4. Skill Menu\n5. Exit");
            //    var choice = System.Console.ReadLine();

            //    switch (choice)
            //    {
            //        case "1":
            //            userMenu.ShowMenu();
            //            break;
            //        case "2":
            //            //courseMenu.ShowMenu();
            //            break;
            //        case "3":
            //            //materialMenu.ShowMenu();
            //            break;
            //        case "4":
            //            //skillMenu.ShowMenu();
            //            break;
            //        case "5":
            //            return; // Exit the application
            //        default:
            //            //Console.WriteLine("Invalid choice. Try again.");
            //            break;
            //    }
            //}
            var services = new ServiceCollection()
            .AddDbContext<EducationPortalContext>(options =>
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EducationPortalDB;Trusted_Connection=True;TrustServerCertificate=True;"))
                .AddScoped<IUserService, UserService>()
                .AddScoped<ICourseService, CourseServicecs>()
                .AddScoped<IMaterialService, MaterialService>()
                .AddScoped<ISkillRepository, SkillRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMaterialRepository, MaterialRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                

                
                .BuildServiceProvider();
            //IDataRepository dataRepository = new FileDataRepository();
            ////DbContext context = new EducationPortalContext();
            //IUserRepository userRepository = new UserRepository(context);
            //ICourseRepository courseRepository = new CourseRepository(context);
         
            //IUserService userService = new UserService(dataRepository,userRepository,courseRepository);
            //ICourseService courseService = new CourseServicecs(dataRepository);
            //IMaterialService materialService = new MaterialService(dataRepository);

            var userService = services.GetService<IUserService>();
            //var courseService = services.GetService<ICourseService>();
            //var materialService = services.GetService<IMaterialService>();

            Menu menu = new Menu(userService);
            menu.Start();
        }
    }
}
