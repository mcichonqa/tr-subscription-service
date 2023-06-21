using SubscriptionService.Application.Repositories;
using SubscriptionService.Contract;
using SubscriptionService.Entity.Models;
using System;
using System.Collections.Generic;
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

        public async Task SetSubscriptionStatusAsync()
        {
            var subscriptionDetails = await _subscriptionRepository.GetAllSubscriptionDetailsAsync();

            var orderedSubscriptionsDetails = subscriptionDetails.Where(x => x.Subscription.SubscriptionStatus != SubscriptionStatus.Expired)
                .GroupBy(
                x => x.SubscriptionId,
                (key, group) => new { Key = key, Collection = group.OrderByDescending(x => x.ExpiredDate).Take(1) })
                .SelectMany(x => x.Collection);

            foreach(var orderedSubscriptionDetails in orderedSubscriptionsDetails)
            {
                if (orderedSubscriptionDetails.ExpiredDate.Date == DateTime.Now.Date)
                    await UpdateSubscriptionStatusAsync(orderedSubscriptionDetails, SubscriptionStatus.Expired);

                if (orderedSubscriptionDetails.ExpiredDate.Date == DateTime.Now.Date.AddDays(1))
                    await UpdateSubscriptionStatusAsync(orderedSubscriptionDetails, SubscriptionStatus.ExpiresSoon);
            }
        }

        private async Task UpdateSubscriptionStatusAsync(SubscriptionDetails orderedSubscriptionDetails, string subscriptionStatus)
        {
            orderedSubscriptionDetails.Subscription.SubscriptionStatus = subscriptionStatus;
            await _subscriptionRepository.UpdateSubscriptionAsync(orderedSubscriptionDetails);
        }
    }
}