using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Enums;
using NewAvalon.Order.Persistence.Constants;

namespace NewAvalon.Order.Persistence.Configurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            ConfigureDataStructure(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.ToTable(TableNames.Orders);

            builder.HasKey(order => order.Id);

            builder.Property(order => order.Id)
                .ValueGeneratedNever()
                .HasConversion(orderId => orderId.Value, value => new OrderId(value));

            builder.Property(order => order.CreatedOnUtc).IsRequired();

            builder.Property(order => order.ModifiedOnUtc);

            builder.Property(order => order.Status).IsRequired().HasDefaultValue(OrderStatus.Shipping);

            builder.Property(order => order.OwnerId).IsRequired();

            builder.Property(order => order.DealerId).IsRequired();

            builder.Property(order => order.DeliveryOnUtc).IsRequired();

            builder.Property(order => order.SerialNumber).IsRequired().ValueGeneratedOnAdd();
        }
    }
}
