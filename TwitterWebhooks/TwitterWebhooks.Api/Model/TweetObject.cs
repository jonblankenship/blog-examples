using System;
using System.Text.Json.Serialization;

namespace TwitterWebhooks.Api.Model
{
    public class TweetObject
    {
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("id_str")]
        public string IdString { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("user")]
        public TwitterUser User { get; set; }
    }
}
