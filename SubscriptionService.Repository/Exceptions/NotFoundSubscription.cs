using System;

namespace SubscriptionService.Repository.Exceptions
{
    public class NotFoundSubscription : Exception
    {
        public NotFoundSubscription(int clientId) : base($"Subscription for client id {clientId} doesn't exist.")
        {
        }
    }
}