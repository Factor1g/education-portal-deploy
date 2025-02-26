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
            var context = new EducationPortalContext();
            IUserRepository userRepository = new UserRepository(context);
            ICourseRepository courseRepository = new CourseRepository(context);
            IMaterialRepository materialRepository = new MaterialRepository(context);
         
            IUserService userService = new UserService(userRepository,courseRepository);
            ICourseService courseService = new CourseServicecs(courseRepository);
            IMaterialService materialService = new MaterialService(materialRepository);

            Menu menu = new Menu(userService, courseService, materialService);
            menu.Start();
        }
    }
}
