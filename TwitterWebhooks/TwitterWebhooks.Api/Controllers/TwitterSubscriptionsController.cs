using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterWebhooks.Api.Services;

namespace TwitterWebhooks.Api.Controllers
{
    [ApiController]
    [Route("api/twitter/subscriptions")]
    public class TwitterSubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public TwitterSubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionsService.GetSubscriptionsAsync();
            
            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<IActionResult> PostSubscriptionAsync()
        {
            await _subscriptionsService.CreateSubscriptionAsync();

            return NoContent();
        }
    }
}
