using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Events;
using NewAvalon.Catalog.Domain.ValueObjects;
using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Catalog.Domain.Entities
{
    public sealed class Product : AggregateRoot<ProductId>, IAuditableEntity
    {
        public Product(ProductId id, string name, decimal price, decimal capacity, string description, Guid creatorId)
            : base(id)
        {
            Name = name;
            Price = price;
            Capacity = capacity;
            Description = description;
            CreatorId = creatorId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Product()
        {
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public decimal Capacity { get; private set; }

        public string Description { get; private set; }

        public ProductImage ProductImage { get; private set; }

        public Guid CreatorId { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public void ChangeProductImage(ProductImage productImage)
        {
            if (ProductImage == productImage)
            {
                return;
            }

            ProductImage ??= ProductImage.Empty;

            if (ProductImage != ProductImage.Empty)
            {
                RaiseDomainEvent(new ProductImageChangedDomainEvent(ProductImage.Id));
            }

            ProductImage = productImage;
        }

        public void RemoveProductImage() => ProductImage = ProductImage.Empty;

        public void Update(string name, decimal price, decimal capacity, string description)
        {
            Name = name;
            Price = price;
            Capacity = capacity;
            Description = description;
        }

        public void ProductAdded(decimal quantity)
        {
            Capacity -= quantity;
        }

        public void Deactivate() => IsActive = false;

        public void IncreaseCapacity(decimal quantity) => Capacity += quantity;
    }
}

