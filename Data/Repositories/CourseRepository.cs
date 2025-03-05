using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
namespace Data.Repositories
{
    public class CourseRepository : EfBaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(EducationPortalContext context) : base(context)
        {
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await context.Set<Course>()
                .Include(c => c.Materials)
                .Include(c => c.Skills)
                .ToListAsync();
        }

        public async Task<List<Course>> GetInProgressCourses(int userId)
        {
            return await context.Set<User>()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.InProgressCourses)
                .Include(c => c.Materials)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCompletedCourses(int userId)
        {
            return await context.Set<User>()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.CompletedCourses)
                .ToListAsync();
        }
        public async Task<bool> EnrollInCourse(int userId, int courseId)
        {
            var user = await context.Set<User>().Include(u => u.InProgressCourses).FirstOrDefaultAsync(u => u.Id == userId);
            var course = await context.Set<Course>().FirstOrDefaultAsync(c => c.Id == courseId);

            if (user == null || course == null)
            {
                throw new CourseNotFoundException("No course was found with given ID!");
                return false;
            }


            if (!user.InProgressCourses.Contains(course))
            {
                user.InProgressCourses.Add(course);
                return await Save();
            }
            return false;
        }

        public async Task<List<Material>> GetAllCourseMaterials(int courseId)
        {
            var course = await context.Set<Course>()
                .Include(c => c.Materials)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            return course?.Materials?.ToList() ?? new List<Material>();
        }

        public async Task<bool> AddCompletedCourse(int userId, int courseId)
        {
            var user = await context.Set<User>().Include(u => u.CompletedCourses).FirstOrDefaultAsync(u => u.Id == userId);
            var course = await context.Set<Course>().FirstOrDefaultAsync(c => c.Id == courseId);

            if (user == null || course == null)
            {
                throw new CourseNotFoundException("No course was found with given ID!");
                return false;
            }

            if (!user.CompletedCourses.Contains(course))
            {
                user.CompletedCourses.Add(course);
                await context.SaveChangesAsync();
                
                return true;
            }
            return false;
        }

        public async Task<List<Skill>> GetAllCourseSkills(int courseId)
        {
            var course = await context.Set<Course>()
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            return course?.Skills?.ToList() ?? new List<Skill>();
        }
    }
}
