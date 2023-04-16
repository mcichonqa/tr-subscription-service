using System;

namespace SubscriptionService.Contract
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public string SubscriptionName { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int ClientId { get; set; }
    }
}