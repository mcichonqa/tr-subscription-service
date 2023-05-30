using MassTransit;
using SharedModels.Events;
using SubscriptionService.Application.Repositories;
using SubscriptionService.Entity.Models;
using System.Threading.Tasks;

namespace SubscriptionService.Api.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public UserCreatedConsumer(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = context.Message;

            await _subscriptionRepository.CreateSubscriptionAsync(new Subscription
            {
                SubscriptionStatus = message.SubscriptionStatus,
                ClientId = message.ClientId
            });
        }
    }
}