using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterWebhooks.Api.Services;

namespace TwitterWebhooks.Api.Controllers
{
    [ApiController]
    [Route("api/twitter/webhooks")]
    public class TwitterWebhooksController : ControllerBase
    {
        private readonly IWebhooksService _webhooksService;

        public TwitterWebhooksController(IWebhooksService webhooksService)
        {
            _webhooksService = webhooksService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWebhookAsync()
        {
            await _webhooksService.CreateWebhookAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWebhooksAsync()
        {
            var webhooks = await _webhooksService.GetAllWebhooksAsync();

            return Ok(webhooks);
        }

        [HttpPut]
        [Route("{webhookId}/crc-check")]
        public async Task<IActionResult> TriggerCrcCheckAsync(long webhookId)
        {
            await _webhooksService.TriggerCrcCheckAsync(webhookId);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllWebhooksAsync()
        {
            await _webhooksService.DeleteAllWebhooksAsync();

            return NoContent();
        }
    }
}
