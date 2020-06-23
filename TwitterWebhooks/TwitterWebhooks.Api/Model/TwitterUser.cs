using System.Text.Json.Serialization;

namespace TwitterWebhooks.Api.Model
{
    public class TwitterUser
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
