
namespace SubscriptionService.Package
{
    public class Package
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<string> Details { get; set; }
        public Price Price { get; set; }
    }

    public class Price
    {
        public double NetValue { get; set; }
        public double GrossValue { get; set; }
        public double TotalValue { get; set; }
    }
}