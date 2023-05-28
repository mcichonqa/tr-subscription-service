
namespace SubscriptionService.Contract
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public SubscriptionDetailsDto? ActiveSubscription { get; set; }
        public string SubscriptionStatus { get; set; }
        public int ClientId { get; set; }
    }
}