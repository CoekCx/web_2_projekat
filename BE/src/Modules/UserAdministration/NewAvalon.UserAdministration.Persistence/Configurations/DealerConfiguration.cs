using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Enums;
using NewAvalon.UserAdministration.Persistence.Constants;

namespace NewAvalon.UserAdministration.Persistence.Configurations
{
    internal sealed class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            ConfigureDataStructure(builder);

            ConfigureRelationships(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<Dealer> builder)
        {
            builder.ToTable(TableNames.Dealers);

            builder.HasKey(dealer => dealer.Id);

            builder.Property(dealer => dealer.Id)
                .ValueGeneratedNever()
                .HasConversion(dealerId => dealerId.Value, value => new UserId(value));

            builder.Property(dealer => dealer.CreatedOnUtc).IsRequired();

            builder.Property(dealer => dealer.ModifiedOnUtc);

            builder.Property(topic => topic.Status).IsRequired().HasDefaultValue(DealerStatus.Pending);
        }

        private static void ConfigureRelationships(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(dealer => dealer.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
