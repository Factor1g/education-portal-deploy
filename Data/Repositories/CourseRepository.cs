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
        public CourseRepository(DbContext context) : base(context)
        {
        }
    }
}
