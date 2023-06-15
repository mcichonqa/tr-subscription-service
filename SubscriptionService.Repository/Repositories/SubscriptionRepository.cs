using Microsoft.EntityFrameworkCore;
using SubscriptionService.Entity;
using SubscriptionService.Entity.Models;
using SubscriptionService.Repository.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionContext _dbContext;

        public SubscriptionRepository(SubscriptionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Subscription>> GetSubscriptionsAsync() => await _dbContext.Subscriptions.Include(x => x.SubscriptionDetails)
            .ToListAsync();

        public async Task<Subscription> GetSubscriptionAsync(int clientId)
        {
            var subscription = await _dbContext.Subscriptions.Include(x => x.SubscriptionDetails)
                .FirstOrDefaultAsync(x => x.ClientId == clientId);

            if (subscription is null)
                throw new NotFoundSubscription(clientId);

            return subscription;
        }

        public async Task CreateSubscriptionAsync(Subscription subscription)
        {
            await _dbContext.Subscriptions.AddAsync(subscription);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<SubscriptionDetails>> GetSubscriptionDetailsAsync(int subscritpionId)
        {
            var subscriptionDetails = await _dbContext.SubscriptionDetails.Where(x => x.SubscriptionId == subscritpionId).ToListAsync();

            return subscriptionDetails;
        }

        public async Task CreateSubscriptionDetailsAsync(SubscriptionDetails subscriptionDetails)
        {
            await _dbContext.SubscriptionDetails.AddAsync(subscriptionDetails);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<SubscriptionDetails>> GetAllSubscriptionDetailsAsync() => await _dbContext.SubscriptionDetails.Include(x => x.Subscription)
            .AsNoTracking()
            .ToListAsync();

        public async Task UpdateSubscriptionAsync(SubscriptionDetails subscriptionDetails)
        {
            _dbContext.SubscriptionDetails.Update(subscriptionDetails);
            await _dbContext.SaveChangesAsync();
        }
    }
}