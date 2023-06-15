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
            List<string> constraintStatuses = new() { SubscriptionStatus.Expired, SubscriptionStatus.ExpiresSoon };

            var subscriptionDetails = await _subscriptionRepository.GetAllSubscriptionDetailsAsync();

            subscriptionDetails = subscriptionDetails.Where(x => !constraintStatuses.Contains(x.Subscription.SubscriptionStatus))
                .ToList();

            var orderedSubscriptionsDetails = subscriptionDetails.GroupBy(
                x => x.SubscriptionId,
                (key, group) => new { Key = key, Collection = group.OrderByDescending(x => x.ExpiredDate).Take(1) })
                .SelectMany(x => x.Collection);

            foreach(string status in constraintStatuses)
                await UpdateSubscriptionStatusAsync(orderedSubscriptionsDetails, status);
        }

        private async Task UpdateSubscriptionStatusAsync(IEnumerable<SubscriptionDetails> orderedSubscriptionDetails, string subscriptionStatus)
        {
            List<SubscriptionDetails> subscriptionForUpdate = new List<SubscriptionDetails>();

            switch (subscriptionStatus)
            {
                case SubscriptionStatus.Expired:
                    subscriptionForUpdate = orderedSubscriptionDetails.Where(x => x.ExpiredDate.Date == DateTime.Now.Date)
                        .ToList();
                    break;
                case SubscriptionStatus.ExpiresSoon:
                    subscriptionForUpdate = orderedSubscriptionDetails.Where(x => x.ExpiredDate.Date == DateTime.Now.Date.AddDays(1))
                        .ToList();
                    break;
                default:
                    throw new Exception($"Urecognized subscription status {subscriptionStatus}.");

            }

            foreach (var subscription in subscriptionForUpdate)
            {
                subscription.Subscription.SubscriptionStatus = subscriptionStatus;
                await _subscriptionRepository.UpdateSubscriptionAsync(subscription);
            }
        }
    }
}