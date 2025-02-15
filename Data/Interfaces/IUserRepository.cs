using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetUserByUsername(string username);
    }

}
