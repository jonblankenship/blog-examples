using System.Threading.Tasks;
using TwitterWebhooks.Api.Model;

namespace TwitterWebhooks.Api.Services
{
    public interface ISubscriptionsService
    {
        public Task CreateSubscriptionAsync();

        Task<SubscriptionsWrapper> GetSubscriptionsAsync();
    }
}
