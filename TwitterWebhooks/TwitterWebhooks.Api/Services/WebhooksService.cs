using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterWebhooks.Api.Configuration;
using TwitterWebhooks.Api.Model;

namespace TwitterWebhooks.Api.Services
{
    public class WebhooksService : IWebhooksService
    {
        private readonly ITwitterClient _twitterClient;
        private readonly TwitterOptions _twitterOptions;

        public WebhooksService(
            ITwitterClient twitterClient,
            TwitterOptions twitterOptions)
        {
            _twitterClient = twitterClient;
            _twitterOptions = twitterOptions;
        }

        public Task<List<Webhook>> GetAllWebhooksAsync() =>
            _twitterClient.GetAsync<List<Webhook>>($"/1.1/account_activity/all/{_twitterOptions.WebhookEnvironment}/webhooks.json");

        public async Task CreateWebhookAsync()
        {
            var callbackUrl = $"{_twitterOptions.WebhookCallbackBaseUrl}/api/twitter/webhook";

            var data = new Dictionary<string, string> {
                { "url", callbackUrl}
            };

            await _twitterClient.PostAsync($"/1.1/account_activity/all/{_twitterOptions.WebhookEnvironment}/webhooks.json", data);
        }

        public async Task DeleteAllWebhooksAsync()
        {
            var webhooks = await GetAllWebhooksAsync();
            foreach (var webhook in webhooks)
            {
                await _twitterClient.DeleteAsync($"/1.1/account_activity/all/{_twitterOptions.WebhookEnvironment}/webhooks/{webhook.Id}.json");
            }
        }

        public Task TriggerCrcCheckAsync(long webhookId) =>
            _twitterClient.PutAsync($"/1.1/account_activity/all/{_twitterOptions.WebhookEnvironment}/webhooks/{webhookId}.json", new Dictionary<string, string>());
    }
}
