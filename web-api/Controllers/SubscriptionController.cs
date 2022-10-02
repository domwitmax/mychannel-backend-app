using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Interfaces.Services;
using application.Models.Subscription;

namespace web_api.Controllers
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
        public IActionResult GetSubscriptions([FromRoute] int userId)
        {
            IEnumerable<SubscriptionDto> subscriptions = new List<SubscriptionDto>();
            return Ok(subscriptions);
        }
        [HttpGet("Count")]
        public IActionResult GetSubscriptionsCount([FromRoute] int userId)
        {
            IEnumerable<SubscriptionDto> subscriptions = new List<SubscriptionDto>();
            int count = subscriptions.Count();
            return Ok(count);
        }
        [HttpPost()]
        public IActionResult AddSubscription([FromRoute] int userId)
        {
            SubscriptionDto subscription = new SubscriptionDto();
            return Ok(subscription);
        }
    }
}
