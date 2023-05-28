using MassTransit;

namespace SubscriptionService.Api.Consumers
{
    public class UserCreatedConsumerDefinition : ConsumerDefinition<UserCreatedConsumer>
    {
        public UserCreatedConsumerDefinition()
        {
            EndpointName = "user.created.queue";
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UserCreatedConsumer> consumerConfigurator)
            => endpointConfigurator.UseInMemoryOutbox();
    }
}