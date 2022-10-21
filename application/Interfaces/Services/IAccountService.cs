using Application.Models.Account;

namespace Application.Interfaces.Services
{
    public interface IAccountService
    {
        string? Login(LoginDto userDto);
        string? Register(RegisterDto registerDto);
        GetUserDto? GetUser(string userName);
        UserDto? Update(UpdateDto updateDto);
    }
}
