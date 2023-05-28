using System;
using System.Collections.Generic;

namespace SubscriptionService.Contract
{
    public class SubscriptionPackageDto
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public List<string> Details { get; set; }
        public PriceDto Price { get; set; }
    }
}
