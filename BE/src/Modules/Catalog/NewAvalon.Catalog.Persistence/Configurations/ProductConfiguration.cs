using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Persistence.Constants;

namespace NewAvalon.Catalog.Persistence.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) =>
            ConfigureDataStructure(builder);

        private static void ConfigureDataStructure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TableNames.Products);

            builder.HasKey(product => product.Id);

            builder.Property(product => product.Id)
                .ValueGeneratedNever()
                .HasConversion(productId => productId.Value, value => new ProductId(value));

            builder.Property(product => product.CreatedOnUtc).IsRequired();

            builder.Property(product => product.ModifiedOnUtc);

            builder.Property(product => product.CreatedOnUtc).IsRequired();

            builder.Property(product => product.Description).IsRequired().HasDefaultValue(string.Empty);

            builder.Property(product => product.CreatorId).IsRequired();

            builder.Property(product => product.IsActive).IsRequired().HasDefaultValue(true);

            builder.OwnsOne(product => product.ProductImage, productImageBuilder =>
            {
                productImageBuilder.Property(productImage => productImage.Id).HasColumnName("ProductImageId");

                productImageBuilder.Property(productImage => productImage.Url).IsRequired().HasColumnName("ProductImageUrl");
            });

            builder.Navigation(user => user.ProductImage).IsRequired();
        }
    }
}
