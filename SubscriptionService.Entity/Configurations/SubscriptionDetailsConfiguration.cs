using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Entity.Models;

namespace SubscriptionService.Entity.Configurations
{
    public class SubscriptionDetailsConfiguration : IEntityTypeConfiguration<SubscriptionDetails>
    {
        public void Configure(EntityTypeBuilder<SubscriptionDetails> builder)
        {
            builder
                .ToTable("SubscriptionDetails")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .UseIdentityColumn()
                .IsRequired();

            builder
                .Property(x => x.SubscriptionName)
                .IsRequired();

            builder
                .Property(x => x.ExpiredDate)
                .HasColumnType("datetime2");

            builder
                .Property(x => x.PurchaseDate)
                .HasColumnType("datetime2");
        }
    }
}