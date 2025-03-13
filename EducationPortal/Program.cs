using Application;
using Console;
using Data;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EducationPortal
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //IDataRepository dataRepository = new FileDataRepository();
            //var context = new EducationPortalContext();
            //IUserRepository userRepository = new UserRepository(context);
            //ICourseRepository courseRepository = new CourseRepository(context);
            //IMaterialRepository materialRepository = new MaterialRepository(context);

            //IUserService userService = new UserService(userRepository,courseRepository);
            //ICourseService courseService = new CourseService(courseRepository);
            //IMaterialService materialService = new MaterialService(materialRepository);

            //var builder = Console.DependencyInjection.AddEducationPortalDependencies(args);
            //var configuration = builder.Configuration; // This retrieves the IConfiguration instance

            //builder.Services.AddEducationPortalDependencies(configuration); // Pass to DI


            //IMenu menu = new Menu(userService, courseService, materialService);
            //menu.Start();
            
                System.Console.WriteLine("App starting...");
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) 
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
                    .Build();

                var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {

                        IConfiguration configuration = context.Configuration;

                        services.AddEducationPortalDependencies(configuration);
                    })
                    .Build();
                await host.StartAsync();
             
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    
                   
                    var dbContext = services.GetRequiredService<EducationPortalContext>();
                    await dbContext.Database.MigrateAsync();

                     
                    var menu = services.GetRequiredService<IMenu>();
                    System.Console.WriteLine("Starting Menu...");
                    await menu.Start();
                    
                }
                await host.WaitForShutdownAsync();

            
        }
    }
}
