using application.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using application.Models.Account;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            return Ok("token");
        }
        [HttpPost]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            return Ok("token");
        }
        [HttpGet("{user}")]
        public IActionResult GetUser([FromRoute] string user)
        {
            UserDto userDto = new UserDto();
            return Ok(userDto);
        }
        [HttpPatch("{user}")]
        public IActionResult Update([FromBody] UpdateDto updateDto)
        {
            return Ok(updateDto);
        }
    }
}
