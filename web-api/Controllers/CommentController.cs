using application.Models.Comment;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController: ControllerBase
    {
        [HttpGet("{videoId}")]
        public IActionResult GetComments([FromRoute] int videoId)
        {
            IEnumerable<CommentDto> comments = new List<CommentDto>();
            return Ok(comments);
        }
        [HttpPost("{videoId}")]
        public IActionResult AddComment([FromRoute] int videoId)
        {
            return Ok();
        }
    }
}
