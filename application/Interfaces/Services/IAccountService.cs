using Application.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
