using Microsoft.AspNetCore.Mvc;
using Application.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces.Services;

namespace Web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            string? jwt = _accountService.Login(loginDto);
            if (jwt == null)
                return BadRequest();
            return Ok(jwt);
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            string? jwt = _accountService.Register(registerDto);
            if (jwt == null)
                BadRequest();
            return Ok(jwt);
        }
        [HttpGet("GetUser/{userName}")]
        [Authorize]
        [ProducesResponseType(typeof(GetUserDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(string userName)
        {
            GetUserDto? userDto = _accountService.GetUser(userName);
            if(userDto == null)
                return NotFound();
            return Ok(userDto);
        }
        [HttpPatch("Update")]
        [Authorize]
        [ProducesResponseType(typeof(UpdateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] UpdateDto updateDto)
        {
            UserDto? userDto = _accountService.Update(updateDto);
            if (userDto == null)
                return NotFound();
            return Ok(updateDto);
        }
    }
}
