using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitterWebhooks.Api.Model;
using Newtonsoft.Json;
using TwitterWebhooks.Api.Configuration;

namespace TwitterWebhooks.Api.Services
{
    public class TwitterClient : ITwitterClient
    {
        private readonly HttpClient _httpClient;
        private readonly TwitterOptions _twitterOptions;
        private readonly HMACSHA1 _sigHasher;
        private readonly DateTime _epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private const string OauthKeyPrefix = "oauth_";

        private Token _bearerToken;

        public TwitterClient(
            IHttpClientFactory httpClientFactory,
            TwitterOptions twitterOptions)
        {
            _httpClient = httpClientFactory.CreateClient();
            _twitterOptions = twitterOptions;

            _httpClient.BaseAddress = new Uri(twitterOptions.TwitterApiUrl);

            _sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes($"{_twitterOptions.ConsumerSecret}&{_twitterOptions.AccessTokenSecret}"));

            _httpClient.BaseAddress = new Uri(_twitterOptions.TwitterApiUrl);
        }

        public async Task<TResource> GetAsync<TResource>(string url)
        {
            var bearerToken = await GetBearerTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", bearerToken.AccessToken);

            var getWebhooksResult = await _httpClient.GetAsync(url);
            getWebhooksResult.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TResource>(await getWebhooksResult.Content.ReadAsStringAsync());
        }

        public async Task PostAsync(string url, Dictionary<string, string> data)
        {
            HmacSignRequest(url, data, HttpMethod.Post);

            // Build the form data (exclude OAuth stuff that's already in the header).
            var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith(OauthKeyPrefix)));
            
            var httpResponse = await _httpClient.PostAsync(url, formData);

            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task PutAsync(string url, Dictionary<string, string> data)
        {
            HmacSignRequest(url, data, HttpMethod.Put);

            // Build the form data (exclude OAuth stuff that's already in the header).
            var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith(OauthKeyPrefix)));

            var httpResponse = await _httpClient.PutAsync(url, formData);

            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            HmacSignRequest(url, new Dictionary<string, string>(), HttpMethod.Delete);

            var httpResponse = await _httpClient.DeleteAsync(url);

            httpResponse.EnsureSuccessStatusCode();
        }

        private async Task<Token> GetBearerTokenAsync()
        {
            if (_bearerToken != null) return _bearerToken;

            var keyValues = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("grant_type", "client_credentials") };
            var content = new FormUrlEncodedContent(keyValues);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Base64Encode($"{HttpUtility.UrlEncode(_twitterOptions.ConsumerKey)}:{HttpUtility.UrlEncode(_twitterOptions.ConsumerSecret)}"));

            var response = await _httpClient.PostAsync("/oauth2/token", content);
            if (response.IsSuccessStatusCode)
            {
                _bearerToken = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
                return _bearerToken;
            }

            return null;
        }

        private void HmacSignRequest(string url, Dictionary<string, string> data, HttpMethod httpMethod)
        {
            // Timestamps are in seconds since 1/1/1970.
            var timestamp = (int)(DateTime.UtcNow - _epochUtc).TotalSeconds;

            // Add all the OAuth headers we'll need to use when constructing the hash.
            data.Add($"{OauthKeyPrefix}consumer_key", _twitterOptions.ConsumerKey);
            data.Add($"{OauthKeyPrefix}signature_method", "HMAC-SHA1");
            data.Add($"{OauthKeyPrefix}timestamp", timestamp.ToString());
            data.Add($"{OauthKeyPrefix}nonce", Guid.NewGuid().ToString("N"));
            data.Add($"{OauthKeyPrefix}token", _twitterOptions.AccessToken);
            data.Add($"{OauthKeyPrefix}version", "1.0");

            // Generate the OAuth signature and add it to our payload.
            data.Add($"{OauthKeyPrefix}signature", GenerateSignature($"{_twitterOptions.TwitterApiUrl}{url}", data, httpMethod));

            // Build the OAuth HTTP Header from the data.
            var oAuthHeader = GenerateOAuthHeader(data);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", oAuthHeader);
        }

        /// <summary>
        /// Generate an OAuth signature from OAuth header values.
        /// </summary>
        private string GenerateSignature(string url, Dictionary<string, string> data, HttpMethod httpMethod)
        {
            var sigString = string.Join(
                "&",
                data
                    .Union(data)
                    .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}")
                    .OrderBy(s => s)
            );

            var fullSigData = $"{httpMethod.Method.ToUpper()}&{Uri.EscapeDataString(url)}&{Uri.EscapeDataString(sigString)}";

            return Convert.ToBase64String(_sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData)));
        }

        /// <summary>
        /// Generate the raw OAuth HTML header from the values (including signature).
        /// </summary>
        private string GenerateOAuthHeader(Dictionary<string, string> data)
        {
            return string.Join(
                       ", ",
                       data
                           .Where(kvp => kvp.Key.StartsWith(OauthKeyPrefix))
                           .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}=\"{Uri.EscapeDataString(kvp.Value)}\"")
                           .OrderBy(s => s)
                   );
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
