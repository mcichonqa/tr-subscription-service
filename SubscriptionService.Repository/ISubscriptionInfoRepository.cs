using SubscriptionService.Entity.Models;
using System.Threading.Tasks;

namespace SubscriptionService.Repository
{
    public interface ISubscriptionInfoRepository
    {
        Task<SubscriptionInfo> GetSubscriptionAsync(int clientId);
        Task CreateSubscriptionAsync(SubscriptionInfo subscriptionIfno);
    }
}