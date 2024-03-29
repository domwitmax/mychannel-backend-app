﻿using Microsoft.AspNetCore.Mvc;
using Application.Models.Video;
using Application.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RankingController: ControllerBase
    {
        private readonly IGetVideoService _getVideoService;
        private readonly IRankingService _rankingService;
        public RankingController(IGetVideoService getVideoService, IRankingService rankingService)
        {
            _getVideoService = getVideoService;
            _rankingService = rankingService;
        }
        private int? getUserId()
        {
            string? userIdStr = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
                return null;
            return userId;
        }
        [HttpGet("View/{videoId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        public IActionResult GetViews([FromRoute] int videoId)
        {
            int views = _rankingService.GetViews(videoId);
            return Ok(views);
        }
        [HttpGet("Like/{videoId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult GetLike([FromRoute] int videoId)
        {
            int like = _rankingService.GetLikes(videoId);
            return Ok(like);
        }
        [HttpPost("Like/{videoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddLike([FromRoute] int videoId)
        {
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            bool like = _rankingService.AddLike(videoId, userId.Value);
            if(!like)
                return BadRequest();
            return Ok();
        }
        [HttpGet("Dislike/{videoId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult GetDislike([FromRoute] int videoId)
        {
            int dislike = _rankingService.GetDislikes(videoId);
            return Ok(dislike);
        }
        [HttpPost("Dislike/{videoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddDislike([FromRoute] int videoId)
        {
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            bool result = _rankingService.AddDislike(videoId, userId.Value);
            if (!result)
                return BadRequest();
            return Ok();
        }
        [HttpGet("GetProposingVideos")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<FullVideoDto>),StatusCodes.Status200OK)]
        public IActionResult GetProposingVideos()
        {
            int? userId = getUserId();
            return Ok(_getVideoService.VideoProposing(userId));
        }
        [HttpGet("Search/{searchKey}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<FullVideoDto>),StatusCodes.Status200OK)]
        public IActionResult Search([FromRoute] string searchKey)
        {
            int? userId = getUserId();
            return Ok(_getVideoService.Search(searchKey, userId));
        }
        [HttpGet("IsLiked/{videoId}")]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult IsLiked([FromRoute] int videoId)
        {
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            bool? isLiked = _rankingService.IsLiked(videoId, userId.Value);
            if (isLiked == null)
                return Ok(0);
            if (isLiked == true)
                return Ok(1);
            else
                return Ok(-1);
        }
        [HttpPost("AddView/{videoId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddView([FromRoute] int videoId)
        {
            int? userId = getUserId();
            bool view = _rankingService.AddView(videoId, userId);
            if (!view)
                return BadRequest();
            return Ok();
        }
    }
}
