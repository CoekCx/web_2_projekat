using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Persistence.Constants;

namespace NewAvalon.UserAdministration.Persistence.Configurations
{
    internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            ConfigureDataStructure(builder);

            ConfigureRelationships(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(TableNames.Clients);

            builder.HasKey(client => client.Id);

            builder.Property(client => client.Id)
                .ValueGeneratedNever()
                .HasConversion(clientId => clientId.Value, value => new UserId(value));

            builder.Property(dealer => dealer.CreatedOnUtc).IsRequired();

            builder.Property(dealer => dealer.ModifiedOnUtc);
        }

        private static void ConfigureRelationships(EntityTypeBuilder<Client> builder)
        {
            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Client>(client => client.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
