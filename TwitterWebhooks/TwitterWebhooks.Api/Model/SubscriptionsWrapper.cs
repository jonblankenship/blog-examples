using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitterWebhooks.Api.Model
{
    public class SubscriptionsWrapper
    {
        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("application_id")]
        public long ApplicationId { get; set; }

        [JsonProperty("subscriptions")]
        public List<Subscription> Subscriptions { get; set; }
    }
}
