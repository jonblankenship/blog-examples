using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitterWebhooks.Api.Services
{
    public interface ITwitterClient
    {
        Task<TResource> GetAsync<TResource>(string url);

        Task PostAsync(string url, Dictionary<string, string> data);

        Task PutAsync(string url, Dictionary<string, string> data);

        Task DeleteAsync(string url);
    }
}
