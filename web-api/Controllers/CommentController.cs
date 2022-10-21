using Application.Interfaces.Services;
using Application.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("{videoId}")]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetComments([FromRoute] int videoId)
        {
            IEnumerable<CommentDto> comments = _commentService.GetComments(videoId);
            return Ok(comments);
        }
        [HttpPost("{videoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddComment([FromRoute] int videoId, [FromBody] CreatedCommentDto commentDto)
        {
            bool result = _commentService.AddComment(videoId, commentDto);
            if (result)
                return NoContent();
            return BadRequest();
        }
    }
}
