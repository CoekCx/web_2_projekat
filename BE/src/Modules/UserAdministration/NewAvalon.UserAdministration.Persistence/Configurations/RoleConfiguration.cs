using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Persistence.Constants;

namespace NewAvalon.UserAdministration.Persistence.Configurations
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder) => ConfigureDataStructure(builder);

        private static void ConfigureDataStructure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableNames.Roles);

            builder.HasKey(role => role.Id);

            builder.Property(role => role.Id).HasConversion(roleId => roleId.Value, value => new RoleId(value));

            builder.Property(role => role.Name).HasMaxLength(50).IsRequired();

            builder.Property(role => role.Description).HasMaxLength(250).IsRequired();

            builder.Property(role => role.CreatedOnUtc).IsRequired();

            builder.Property(role => role.ModifiedOnUtc);
        }
    }
}
