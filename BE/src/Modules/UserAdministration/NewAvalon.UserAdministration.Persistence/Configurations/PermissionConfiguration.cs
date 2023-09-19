using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Persistence.Constants;

namespace NewAvalon.UserAdministration.Persistence.Configurations
{
    public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            ConfigureDataStructure(builder);

            ConfigureRelationships(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableNames.Permissions);

            builder.HasKey(permission => permission.Id);

            builder.Property(permission => permission.Id).HasConversion(permission => permission.Value, value => new PermissionId(value));

            builder.Property(permission => permission.Id)
                .ValueGeneratedNever();

            builder.Property(permission => permission.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(permission => permission.Description)
                .HasMaxLength(250);
        }

        private static void ConfigureRelationships(EntityTypeBuilder<Permission> builder) =>
            builder.HasMany(permission => permission.Roles).WithMany(role => role.Permissions);
    }
}
