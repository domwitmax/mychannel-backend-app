using Microsoft.AspNetCore.Mvc;
using application.Models.Video;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/{videoId}")]
    public class RankingController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetViews([FromRoute] int videoId)
        {
            int views = 0;
            return Ok(views);
        }
        [HttpPost]
        public IActionResult AddView([FromRoute] int video)
        {
            int view = 0;
            return Ok(view);
        }
        [HttpGet("Like")]
        public IActionResult GetLike([FromRoute] int videoId)
        {
            int like = 0;
            return Ok(like);
        }
        [HttpPost("Like")]
        public IActionResult AddLike([FromRoute] int videoId)
        {
            var like = 0;
            return Ok(like);
        }
        [HttpGet("Dislike")]
        public IActionResult GetDislike([FromRoute] int videoId)
        {
            int dislike = 0;
            return Ok(dislike);
        }
        [HttpPost("Dislike")]
        public IActionResult AddDislike([FromRoute] int videoId)
        {
            int dislike = 0;
            return Ok(dislike);
        }
        [HttpGet("GetProposingVideos")]
        public IActionResult GetProposingVideos([FromRoute] int videoId)
        {
            IEnumerable<VideoDto> videos = new List<VideoDto>();
            return Ok(videos);
        }
    }
}
