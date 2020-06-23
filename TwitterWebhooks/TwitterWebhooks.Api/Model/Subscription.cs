using Newtonsoft.Json;

namespace TwitterWebhooks.Api.Model
{
    public class Subscription
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }
}
