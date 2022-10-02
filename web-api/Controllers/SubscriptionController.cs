using application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.Interfaces.Services;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Subscription>> GetSubscriptions()
        {
            return Ok(_subscriptionService.GetSubscriptions());
        }
    }
}
