using application.Data;
using Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Repository
{
    public class AccountRepository: Application.Interfaces.Repository.IAccountRepository
    {
        MyChannelDbContext _context;
        IMapper _mapper;
        public AccountRepository(MyChannelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddUser(User user)
        {
            User? userFromDb = _context.Users.SingleOrDefault(x => x.UserName == user.UserName);
            if(userFromDb != null)
                return false;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User? GetUser(int userId)
        {
            User? userFromDb = _context.Users.SingleOrDefault(x => x.UserId == userId);
            return userFromDb;
        }

        public User? GetUser(string userName)
        {
            User? userFromDb = _context.Users.SingleOrDefault(x => x.UserName == userName);
            return userFromDb;
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }
    }
}
