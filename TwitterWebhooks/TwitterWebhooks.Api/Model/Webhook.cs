using System;
using Newtonsoft.Json;

namespace TwitterWebhooks.Api.Model
{
    public class Webhook
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valid")]
        public bool Valid { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
