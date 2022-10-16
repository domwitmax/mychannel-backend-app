using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Models.Subscription;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private int? getUserId()
        {
            string? userIdStr = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
                return null;
            return userId;
        }
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetSubscriptions()
        {
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            IEnumerable<SubscriptionDto> subscriptions = _subscriptionService.GetSubscriptions(userId.Value);
            return Ok(subscriptions);
        }
        [HttpGet("Count")]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        public IActionResult GetSubscriptionsCount()
        {
            int? userId = getUserId();
            if (userId == null)
                return Unauthorized();
            int count = _subscriptionService.GetSubscriptionCount(userId.Value);
            return Ok(count);
        }
        [HttpPost("AddSubsscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSubscription([FromBody] SubscriptionDto subscriptionDto)
        {
            bool result = _subscriptionService.AddSubscription(subscriptionDto);
            if (result)
                return Ok();
            return BadRequest();
        }
        [HttpPost("RemoveSubscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemoveSubscription([FromRoute] int userId, [FromBody] SubscriptionDto subscriptionDto)
        {
            bool result = _subscriptionService.RemoveSubscription(subscriptionDto);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
