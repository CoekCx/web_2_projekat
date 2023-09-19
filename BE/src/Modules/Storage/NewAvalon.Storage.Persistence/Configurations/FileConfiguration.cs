using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Persistence.Constants;

namespace NewAvalon.Storage.Persistence.Configurations
{
    internal sealed class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder) => ConfigureDataStructure(builder);

        private static void ConfigureDataStructure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable(TableNames.Files);

            builder.HasKey(file => file.Id);

            builder.Property(file => file.Id).HasConversion(imageId => imageId.Value, value => new FileId(value));

            builder.Property(file => file.Url).IsRequired();

            builder.Property(file => file.Name).IsRequired();

            builder.Property(file => file.Extension).IsRequired();

            builder.Property(file => file.Size).IsRequired();

            builder.Property(file => file.CreatedOnUtc).IsRequired();

            builder.Property(file => file.ModifiedOnUtc);
        }
    }
}
