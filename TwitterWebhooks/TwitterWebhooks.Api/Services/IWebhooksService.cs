using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterWebhooks.Api.Model;

namespace TwitterWebhooks.Api.Services
{
    public interface IWebhooksService
    {
        public Task CreateWebhookAsync();

        Task<List<Webhook>> GetAllWebhooksAsync();

        public Task DeleteAllWebhooksAsync();

        public Task TriggerCrcCheckAsync(long webhookId);
    }
}
