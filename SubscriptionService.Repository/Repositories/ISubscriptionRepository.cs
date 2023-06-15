using SubscriptionService.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<ICollection<Subscription>> GetSubscriptionsAsync();
        Task<Subscription> GetSubscriptionAsync(int clientId);
        Task CreateSubscriptionAsync(Subscription subscription);
        Task<ICollection<SubscriptionDetails>> GetAllSubscriptionDetailsAsync();
        Task<ICollection<SubscriptionDetails>> GetSubscriptionDetailsAsync(int subscritpionId);
        Task CreateSubscriptionDetailsAsync(SubscriptionDetails subscriptionDetail);
        Task UpdateSubscriptionAsync(SubscriptionDetails subscriptionDetails);
    }
}