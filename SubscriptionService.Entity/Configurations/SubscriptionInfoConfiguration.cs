using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionService.Entity.Models;

namespace SubscriptionService.Entity.Configurations
{
    public class SubscriptionInfoConfiguration : IEntityTypeConfiguration<SubscriptionInfo>
    {
        public void Configure(EntityTypeBuilder<SubscriptionInfo> builder)
        {
            builder
                .ToTable("SubscriptionInfo")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .UseIdentityColumn()
                .IsRequired();

            builder
                .Property(x => x.SubscriptionName)
                .IsRequired();

            builder.Property(x => x.IsEnabled);

            builder.
                 Property(x => x.PurchaseDate)
                .HasColumnType("datetime2")
                .HasMaxLength(20)
                .IsRequired();

            builder.
                 Property(x => x.ExpiredDate)
                .HasColumnType("datetime2")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(x => x.ClientId)
                .IsRequired();
        }
    }
}