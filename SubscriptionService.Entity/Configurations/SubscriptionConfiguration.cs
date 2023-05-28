using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Entity.Models;

namespace SubscriptionService.Entity.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder
                .ToTable("Subscriptions")
                .HasKey(x => x.Id);

            builder
                .HasMany(x => x.SubscriptionDetails)
                .WithOne(x => x.Subscription)
                .HasForeignKey(x => x.SubscriptionId);

            builder
                .Property(x => x.Id)
                .UseIdentityColumn()
                .IsRequired();

            builder
                .Property(x => x.SubscriptionStatus)
                .IsRequired();

            builder
                .Property(x => x.ClientId)
                .IsRequired();
        }
    }
}