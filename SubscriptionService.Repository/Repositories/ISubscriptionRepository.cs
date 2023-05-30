using SubscriptionService.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> GetSubscriptionAsync(int clientId);
        Task CreateSubscriptionAsync(Subscription subscription);
        Task<ICollection<SubscriptionDetails>> GetSubscriptionDetailsAsync(int subscritpionId);
        Task CreateSubscriptionDetailsAsync(SubscriptionDetails subscriptionDetail);
    }
}