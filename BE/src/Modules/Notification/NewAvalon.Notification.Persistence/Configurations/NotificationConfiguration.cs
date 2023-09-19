using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Notification.Domain.EntityIdentifiers;
using NewAvalon.Notification.Persistence.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NewAvalon.Notification.Persistence.Configurations
{
    internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Domain.Entities.Notification>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Notification> builder)
        {
            ConfigureDataStructure(builder);

            ConfigureIndexes(builder);
        }

        private void ConfigureDataStructure(EntityTypeBuilder<Domain.Entities.Notification> builder)
        {
            builder.ToTable(TableNames.Notifications);

            builder.HasKey(notification => notification.Id);

            builder.Property(notification => notification.Id)
                .HasConversion(
                    notificationId => notificationId.Value,
                    value => new NotificationId(value));

            builder.Property(notification => notification.UserId).IsRequired(false);

            builder.Property(notification => notification.Email).HasMaxLength(256).IsRequired(false);

            builder.Property(notification => notification.DeliveryMechanism).IsRequired();

            builder.Property(notification => notification.Type).IsRequired();

            builder.Property(notification => notification.Title).HasMaxLength(300);

            builder.Property(notification => notification.Content).HasMaxLength(1000);

            builder.Property(notification => notification.Details)
                .HasConversion(
                    details => JsonConvert.SerializeObject(details),
                    value => JsonConvert.DeserializeObject<Dictionary<string, object>>(value))
                .HasColumnType("json");

            builder.Property(notification => notification.Published).IsRequired();

            builder.Property(notification => notification.PublishedOnUtc).IsRequired(false);

            builder.Property(notification => notification.Failed).IsRequired();

            builder.Property(notification => notification.FailedOnUtc).IsRequired(false);

            builder.Property(notification => notification.CreatedOnUtc).IsRequired();

            builder.Property(notification => notification.ModifiedOnUtc).IsRequired(false);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Domain.Entities.Notification> builder) =>
            builder
                .HasIndex(x => new { x.Published, x.Failed, x.DeliveryMechanism, x.CreatedOnUtc })
                .HasFilter(@"NOT (""Published"") AND NOT (""Failed"")");
    }
}
