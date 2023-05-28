using System;

namespace SubscriptionService.Contract
{
    public class SubscriptionDetailsDto
    {
        public int Id { get; set; }
        public string SubscriptionName { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SubscriptionId { get; set; }
    }
}