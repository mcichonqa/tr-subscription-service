using SubscriptionService.Application.Repositories;
using SubscriptionService.Contract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Services
{
    public class SubscriptionsService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionsService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<SubscriptionDto> GetSubscription(int clientId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionAsync(clientId);

            var lastActiveSubscription = subscription.SubscriptionDetails.Any()
                ? subscription.SubscriptionDetails.Where(x => x.ExpiredDate >= DateTime.Now).FirstOrDefault()
                : null;

            var subscriptionDto = new SubscriptionDto()
            {
                Id = subscription.Id,
                SubscriptionStatus = subscription.SubscriptionStatus,
                ClientId = clientId
            };

            if (lastActiveSubscription != null)
                subscriptionDto.ActiveSubscription = new SubscriptionDetailsDto()
                {
                    Id = lastActiveSubscription.Id,
                    SubscriptionName = lastActiveSubscription.SubscriptionName,
                    ExpiredDate = lastActiveSubscription.ExpiredDate,
                    PurchaseDate = lastActiveSubscription.PurchaseDate,
                    SubscriptionId = lastActiveSubscription.SubscriptionId
                };

            return subscriptionDto;
        }
    }
}