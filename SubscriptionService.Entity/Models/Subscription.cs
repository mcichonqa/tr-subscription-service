using System.Collections.Generic;

namespace SubscriptionService.Entity.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string SubscriptionStatus { get; set; }
        public int ClientId { get; set; }

        public virtual ICollection<SubscriptionDetails> SubscriptionDetails { get; set; }
    }
}