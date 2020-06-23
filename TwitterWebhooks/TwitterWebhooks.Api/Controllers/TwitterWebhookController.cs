using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TwitterWebhooks.Api.Configuration;
using TwitterWebhooks.Api.Model;

namespace TwitterWebhooks.Api.Controllers
{
    [ApiController]
    [Route("api/twitter/webhook")]
    public class TwitterWebhookController : ControllerBase
    {
        private readonly TwitterOptions _twitterOptions;

        public TwitterWebhookController(TwitterOptions twitterOptions)
        {
            _twitterOptions = twitterOptions;
        }

        [HttpGet]
        public IActionResult Index(string crc_token)
        {
            var hash = GetHash(crc_token, _twitterOptions.ConsumerSecret);
            
            return new OkObjectResult(new { response_token = $"sha256={hash}" });
        }

        [HttpPost]
        public IActionResult PostWebhook([FromBody] TweetEvent tweetEvent)
        {
            // Here's where we'll process the calls from Twitter when there's a new Tweet event.

            return new OkResult();
        }

        private string GetHash(string text, string key)
        {
            var encoding = new ASCIIEncoding();
            var textBytes = encoding.GetBytes(text);
            var keyBytes = encoding.GetBytes(key);

            using var hash = new HMACSHA256(keyBytes);
            
            var hashBytes = hash.ComputeHash(textBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
