using System;

namespace SubscriptionService.Entity.Models
{
    public class SubscriptionInfo
    {
        public int Id { get; set; }
        public string SubscriptionName { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int ClientId { get; set; }
    }
}