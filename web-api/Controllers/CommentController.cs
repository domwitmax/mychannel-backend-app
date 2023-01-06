using Application.Interfaces.Services;
using Application.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private int? getUserId()
        {
            string? userIdStr = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
                return null;
            return userId;
        }
        [HttpGet("{videoId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>),StatusCodes.Status200OK)]
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
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            commentDto.UserId = userId.Value;
            bool result = _commentService.AddComment(videoId, commentDto);
            if (result)
                return NoContent();
            return BadRequest();
        }
    }
}
