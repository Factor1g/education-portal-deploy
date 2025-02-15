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
    public class UserRepository : EfBaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User GetUserByUsername(string username)
        {
            var user = context.Set<User>().FirstOrDefault(x => x.Username == username);
            return user;
        }
    }
}
