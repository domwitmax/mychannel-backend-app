using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Models.Subscription;

namespace Web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/{userId}")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionDto>),StatusCodes.Status200OK)]
        public IActionResult GetSubscriptions([FromRoute] int userId)
        {
            IEnumerable<SubscriptionDto> subscriptions = _subscriptionService.GetSubscriptions(userId);
            return Ok(subscriptions);
        }
        [HttpGet("Count")]
        [ProducesResponseType(typeof(int),StatusCodes.Status200OK)]
        public IActionResult GetSubscriptionsCount([FromRoute] int userId)
        {
            int count = _subscriptionService.GetSubscriptionCount(userId);
            return Ok(count);
        }
        [HttpPost("AddSubsscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddSubscription([FromRoute] int userId, [FromBody] SubscriptionDto subscriptionDto)
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
