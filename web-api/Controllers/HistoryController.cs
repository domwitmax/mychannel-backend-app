﻿using Application.Interfaces.Services;
using Application.Models.History;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class HistoryController: ControllerBase
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }
        [HttpPost("SaveVideoStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveVideoStatus(VideoStatusDto videoStatusDto)
        {
            string? userIdStr = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null)
                return Unauthorized();
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();
            bool result = _historyService.SaveVideoStatus(videoStatusDto,userId);
            if (!result)
                return BadRequest();
            

            return Ok();
        }
        [HttpPost("GetVideoStatus/{videoId}")]
        [ProducesResponseType(typeof(VideoStatusDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetVideoStatus([FromRoute] int videoId)
        {
            VideoStatusDto? videoStatusDto = _historyService.GetVideoStatus(videoId);
            if (videoStatusDto == null)
                return NotFound();
            return Ok(videoStatusDto);
        }
    }
}
