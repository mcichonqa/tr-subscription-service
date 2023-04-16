using Microsoft.EntityFrameworkCore;
using SubscriptionService.Entity;
using SubscriptionService.Entity.Models;
using System.Threading.Tasks;

namespace SubscriptionService.Repository
{
    public class SubscriptionInfoRepository : ISubscriptionInfoRepository
    {
        private readonly SubscriptionContext _dbContext;

        public SubscriptionInfoRepository(SubscriptionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SubscriptionInfo> GetSubscriptionAsync(int clientId)
            => await _dbContext.SubscriptionInfo.FirstOrDefaultAsync(sub => sub.ClientId == clientId);

        public async Task CreateSubscriptionAsync(SubscriptionInfo subscriptionIfno)
        {
            await _dbContext.SubscriptionInfo.AddAsync(subscriptionIfno);
            await _dbContext.SaveChangesAsync();
        }
    }
}