using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IAccountRepository
    {
        User? GetUser(int userId);
        User? GetUser(string userName);
        bool AddUser(User user);
        bool UpdateUser(User user);
    }
}
