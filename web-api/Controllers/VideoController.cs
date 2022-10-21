using Application.Interfaces.Services;
using Application.Models.Video;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VideoController: ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IVideoService _videoService;
        private readonly IRankingService _rankingService;
        public VideoController(IFileService fileService, IVideoService videoService, IRankingService rankingService)
        {
            _fileService = fileService;
            _videoService = videoService;
            _rankingService = rankingService;
        }
        private int? getUserId()
        {
            if (!int.TryParse(User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId))
                return null;
            return userId;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddVideo([FromBody] VideoDto videoDto)
        {
            int? result = _videoService.AddVideo(videoDto);
            if(result == null)
                return BadRequest();
            return Ok(result.Value);
        }
        [HttpPost("LoadVideo/{videoId}")]
        [RequestSizeLimit(10737418240)]
        [RequestFormLimits(MultipartBodyLengthLimit = 10737418240)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult LoadVideo([FromForm] IFormFile file, [FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = _videoService.GetVideo(videoId);
            if (fullVideoDto == null)
                return BadRequest();
            int? userId = getUserId();
            if (userId == null || fullVideoDto.AuthorId != userId)
                return Unauthorized();
            if (fullVideoDto.VideoPath != null)
                return Conflict();
            string? path = _fileService.LoadVideo(file, userId.Value);
            if (path == null)
                return BadRequest();
            _videoService.UpdateVideo(path, videoId);
            return Ok();
        }
        [HttpPost("LoadThumbnail/{videoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult LoadThumbnail([FromForm] IFormFile file, [FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = _videoService.GetVideo(videoId);
            if (fullVideoDto == null)
                return BadRequest();
            int? userId = getUserId();
            if (userId == null || fullVideoDto.AuthorId != userId)
                return Unauthorized();
            if (fullVideoDto.ThumbnailPath != null)
                return Conflict();
            string? path = _fileService.LoadThumbnail(file, userId.Value);
            if (path == null)
                return BadRequest();
            _videoService.UpdateThumbnail(path, videoId);
            return Ok();
        }
        [HttpDelete("{videoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVideo([FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = _videoService.GetVideo(videoId);
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            if (fullVideoDto == null)
                return NotFound();
            if (userId != fullVideoDto.AuthorId)
                return Unauthorized();
            bool result = _videoService.DeleteVideo(videoId);
            if(!result)
                return BadRequest();
            return Ok();
        }
        [HttpGet("{videoId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(FullVideoDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVideo([FromRoute] int videoId)
        {
            FullVideoDto? fullVideoDto = _videoService.GetVideo(videoId);
            if (fullVideoDto == null)
                return NotFound();
            int? userId = getUserId();
            _rankingService.AddView(videoId, userId);
            return Ok(fullVideoDto);
        }
        [HttpGet("GetAllUserVideo/{userName}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<FullVideoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserVideo([FromRoute] string userName)
        {
            IEnumerable<FullVideoDto> videoDtos = _videoService.GetAllVideo(userName);
            if (videoDtos == null)
                return NotFound();
            return Ok(videoDtos);
        }
    }
}
