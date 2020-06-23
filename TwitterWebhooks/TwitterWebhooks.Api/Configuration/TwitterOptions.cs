namespace TwitterWebhooks.Api.Configuration
{
    public class TwitterOptions
    {
        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }

        public string WebhookEnvironment { get; set; }

        public string TwitterApiUrl { get; set; }

        public string WebhookCallbackBaseUrl { get; set; }
    }
}
