using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwitterWebhooks.Api.Model
{
    public class Environment
    {
        [JsonPropertyName("environment_name")]
        public string EnvironmentName { get; set; }

        [JsonPropertyName("webhooks")]
        public List<Webhook> Webhooks { get; set; }
    }
}
