using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Persistence.Constants;

namespace NewAvalon.Storage.Persistence.Configurations
{
    internal sealed class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder) => ConfigureDataStructure(builder);

        private static void ConfigureDataStructure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable(TableNames.Images);

            builder.HasKey(image => image.Id);

            builder.Property(image => image.Id).HasConversion(imageId => imageId.Value, value => new ImageId(value));

            builder.Property(image => image.Url).IsRequired();

            builder.Property(image => image.CreatedOnUtc).IsRequired();

            builder.Property(image => image.ModifiedOnUtc);
        }
    }
}
