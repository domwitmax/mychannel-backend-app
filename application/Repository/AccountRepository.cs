using Application.Data;
using Application.Data.Entities;
using Application.Interfaces.Repository;

namespace Application.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly MyChannelDbContext _context;
        public AccountRepository(MyChannelDbContext context)
        {
            _context = context;
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
