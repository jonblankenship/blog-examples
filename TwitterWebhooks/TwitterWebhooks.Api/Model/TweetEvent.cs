using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwitterWebhooks.Api.Model
{
    public class TweetEvent
    {
        [JsonPropertyName("for_user_id")]
        public string ForUserId { get; set; }

        [JsonPropertyName("user_has_blocked")]
        public bool UserHasBlocked { get; set; }

        [JsonPropertyName("tweet_create_events")]
        public List<TweetObject> TweetCreateEvents { get; set; }
    }
}
