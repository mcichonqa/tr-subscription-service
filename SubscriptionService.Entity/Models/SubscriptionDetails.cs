using System;

namespace SubscriptionService.Entity.Models
{
    public class SubscriptionDetails
    {
        public int Id { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}