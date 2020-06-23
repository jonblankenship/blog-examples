using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterWebhooks.Api.Configuration;
using TwitterWebhooks.Api.Model;

namespace TwitterWebhooks.Api.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly ITwitterClient _twitterClient;
        private readonly TwitterOptions _twitterOptions;

        public SubscriptionsService(
            ITwitterClient twitterClient,
            TwitterOptions twitterOptions)
        {
            _twitterClient = twitterClient;
            _twitterOptions = twitterOptions;
        }

        public Task CreateSubscriptionAsync() =>
            _twitterClient.PostAsync($"/1.1/account_activity/all/{_twitterOptions.WebhookEnvironment}/subscriptions.json", new Dictionary<string, string>());

        public Task<SubscriptionsWrapper> GetSubscriptionsAsync() =>
            _twitterClient.GetAsync<SubscriptionsWrapper>($"/1.1/account_activity/all/{_twitterOptions.WebhookEnvironment}/subscriptions/list.json");
    }
}
