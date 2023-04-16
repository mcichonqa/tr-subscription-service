using Microsoft.EntityFrameworkCore;
using SubscriptionService.Entity.Models;
using System.Reflection;

namespace SubscriptionService.Entity
{
    public class SubscriptionContext : DbContext
    {
        public SubscriptionContext(DbContextOptions<SubscriptionContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<SubscriptionInfo> SubscriptionInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}