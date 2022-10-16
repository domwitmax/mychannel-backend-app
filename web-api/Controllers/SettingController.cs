using Microsoft.AspNetCore.Mvc;
using Application.Models.Setting;
using Application.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SettingController: ControllerBase
    {
        private ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SettingDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSettings()
        {
            string? userIdStr = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr,out int userId))
                return Unauthorized();
            SettingDto? settingDto = _settingService.GetSetting(userId);
            if(settingDto == null)
                return NotFound();
            return Ok(settingDto);
        }
        [HttpPatch("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateSettings([FromRoute] int userId, [FromBody] SettingDto settingDto)
        {
            bool result = _settingService.UpdateSetting(userId, settingDto);
            if (result)
                return NoContent();
            return BadRequest();
        }
    }
}
