using Data.Interfaces;
using Data.Repositories;
using Data;
using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEducationPortalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EducationPortalContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<ISkillService, SkillService>();

            services.AddScoped<IMenu, Menu>();
            services.AddScoped<Menu>();
            return services;
        }
    }
}
