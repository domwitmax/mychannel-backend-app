using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Models.Subscription;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Web_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionController: ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private string? getUserName()
        {
            string? userName = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return userName;
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
            string? userName = getUserName();
            if (userName == null)
                return Unauthorized();
            IEnumerable<SubscriptionDto> subscriptions = _subscriptionService.GetSubscriptions(userName);
            return Ok(subscriptions);
        }
        [HttpGet("Count/{subscriptionUserName}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        public IActionResult GetSubscriptionsCount([FromRoute] string subscriptionUserName)
        {
            int count = _subscriptionService.GetSubscriptionCount(subscriptionUserName);
            return Ok(count);
        }
        [HttpPost("AddSubsscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddSubscription([FromBody] SubscriptionDto subscriptionDto)
        {
            if (getUserName() == null)
                return Unauthorized();
            if (getUserName() != subscriptionDto.UserName || subscriptionDto.UserName == subscriptionDto.SubscriptionUserName)
                return BadRequest();
            bool result = _subscriptionService.AddSubscription(subscriptionDto);
            if (result)
                return Ok();
            return BadRequest();
        }
        [HttpPost("RemoveSubscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult RemoveSubscription([FromBody] SubscriptionDto subscriptionDto)
        {
            if (getUserName() == null)
                return Unauthorized();
            if (getUserName() != subscriptionDto.UserName || subscriptionDto.UserName == subscriptionDto.SubscriptionUserName)
                return BadRequest();
            bool result = _subscriptionService.RemoveSubscription(subscriptionDto);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
