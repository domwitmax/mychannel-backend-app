using Microsoft.AspNetCore.Mvc;
using application.Models.Setting;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/{userId}")]
    public class SettingController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetSettings([FromRoute] int userId)
        {
            SettingDto settingDto = new SettingDto();
            return Ok(settingDto);
        }
        [HttpPatch]
        public IActionResult UpdateSettings([FromRoute] int userId, [FromBody] SettingDto settingDto)
        {
            SettingDto newSettingDto = new SettingDto();
            return Ok(newSettingDto);
        }
    }
}
