using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MaterialRepository : EfBaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(DbContext context) : base(context)
        {
        }
    }
}
