using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AccountService: IAccountService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IAccountRepository _accountRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountService(AuthenticationSettings authenticationSettings, ISettingRepository settingRepository, IFileRepository fileRepository, IAccountRepository accountRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _authenticationSettings = authenticationSettings;
            _accountRepository = accountRepository;
            _fileRepository = fileRepository;
            _settingRepository = settingRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public GetUserDto? GetUser(string userName)
        {
            User? user = _accountRepository.GetUser(userName);
            return _mapper.Map<GetUserDto?>(user);
        }

        public string? Login(LoginDto userDto)
        {
            User? user = _accountRepository.GetUser(userDto.UserName);
            if (user == null)
                return null;
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;
            return generateJwt(user);
        }

        public string? Register(RegisterDto registerDto)
        {
            User? user = _accountRepository.GetUser(registerDto.UserName);
            if (user != null)
                return null;
            User newUser = _mapper.Map<User>(registerDto);
            string passwordHash = _passwordHasher.HashPassword(newUser, registerDto.Password);
            newUser.PasswordHash = passwordHash;
            _accountRepository.AddUser(newUser);
            _fileRepository.AddNewFolder(newUser.UserName);
            Setting setting = new Setting();
            setting.UserId = newUser.UserId;
            _settingRepository.AddSetting(setting);
            return generateJwt(newUser);
        }

        public UserDto? Update(UpdateDto updateDto)
        {
            User? user = _accountRepository.GetUser(updateDto.UserName);
            if (user == null)
                return null;
            if(updateDto.Password != null)
                user.PasswordHash = _passwordHasher.HashPassword(user, updateDto.Password);
            if (updateDto.Email != null)
                user.Email = updateDto.Email;
            _accountRepository.UpdateUser(user);
            return _mapper.Map<UserDto?>(user);
        }

        private string? generateJwt(User userDto)
        {
            var user = userDto;

            if (user is null)
                return null;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
