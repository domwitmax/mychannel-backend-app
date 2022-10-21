using Application.Data.Entities;

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
