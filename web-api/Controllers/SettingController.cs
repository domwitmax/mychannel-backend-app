using Microsoft.AspNetCore.Mvc;
using Application.Models.Setting;
using Application.Interfaces.Services;

namespace Web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController: ControllerBase
    {
        private ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(SettingDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSettings([FromRoute] int userId)
        {
            SettingDto? settingDto = _settingService.GetSetting(userId);
            if(settingDto == null)
                return NotFound();
            return Ok(settingDto);
        }
        [HttpPatch("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSettings([FromRoute] int userId, [FromBody] SettingDto settingDto)
        {
            bool result = _settingService.UpdateSetting(userId, settingDto);
            if (result)
                return NoContent();
            return BadRequest();
        }
    }
}
