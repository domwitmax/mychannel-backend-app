using application.Models.Video;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        [HttpGet("{videoId}")]
        public IActionResult GetVideo([FromRoute] int videoId)
        {
            VideoDto videoDto = new VideoDto();
            return Ok(videoDto);
        }
        [HttpPost]
        public IActionResult UploadVideo([FromBody] FullVideoDto fullVideoDto)
        {
            VideoDto videoDto = new VideoDto();
            return Ok(videoDto);
        }
    }
}
