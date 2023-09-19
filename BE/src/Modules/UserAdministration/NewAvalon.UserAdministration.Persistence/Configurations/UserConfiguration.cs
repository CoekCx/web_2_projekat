using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Persistence.Constants;

namespace NewAvalon.UserAdministration.Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureDataStructure(builder);

            ConfigureRelationships(builder);

            ConfigureIndexes(builder);
        }

        private static void ConfigureDataStructure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.Users);

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).HasConversion(userId => userId.Value, value => new UserId(value));

            builder.Property(user => user.IdentityProviderId).IsRequired(false);

            builder.Property(user => user.FirstName).HasMaxLength(200).IsRequired();

            builder.Property(user => user.LastName).HasMaxLength(200).IsRequired();

            builder.Property(user => user.Email).HasMaxLength(256).IsRequired();

            builder.Property(user => user.CreatedOnUtc).IsRequired();

            builder.Property(user => user.ModifiedOnUtc);

            builder.OwnsOne(user => user.ProfileImage, profileImageBuilder =>
            {
                profileImageBuilder.Property(profileImage => profileImage.Id).HasColumnName("ProfileImageId");

                profileImageBuilder.Property(profileImage => profileImage.Url).IsRequired().HasColumnName("ProfileImageUrl");
            });

            builder.Navigation(user => user.ProfileImage).IsRequired();
        }

        private static void ConfigureRelationships(EntityTypeBuilder<User> builder) =>
            builder.HasMany(user => user.Roles).WithMany(role => role.Users);

        private static void ConfigureIndexes(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
