using NewAvalon.Domain.Abstractions;
using NewAvalon.Order.Domain.EntityIdentifiers;
using NewAvalon.Order.Domain.Enums;
using NewAvalon.Order.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.Order.Domain.Entities
{
    public sealed class Order : AggregateRoot<OrderId>, IAuditableEntity
    {
        private const decimal DeliveryPrice = 20;

        private readonly List<Product> _products = new();

        public Order(
            OrderId id,
            Guid ownerId,
            Guid dealerId,
            string comment,
            string deliveryAddress,
            DateTime deliveryOnUtc) : base(id)
        {
            OwnerId = ownerId;
            DealerId = dealerId;
            Status = OrderStatus.Shipping;
            Comment = comment;
            DeliveryAddress = deliveryAddress;
            DeliveryOnUtc = deliveryOnUtc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Order()
        {
        }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public OrderStatus Status { get; private set; }

        public Guid OwnerId { get; private set; }

        public Guid DealerId { get; private set; }

        public string Comment { get; private set; }

        public string DeliveryAddress { get; private set; }

        public DateTime DeliveryOnUtc { get; private set; }

        public long SerialNumber { get; private set; }

        public IReadOnlyCollection<Product> Products => _products.ToList();

        public Product AddProduct(Guid catalogProductId, decimal price, decimal quantity, string name)
        {
            var productId = new ProductId(Guid.NewGuid());

            var product = new Product(productId, Id, catalogProductId, quantity, price, name);

            _products.Add(product);

            RaiseDomainEvent(new ProductAddedDomainEvent(productId));

            return product;
        }

        public static decimal GetDeliveryPrice() => DeliveryPrice;

        public decimal GetFullPrice()
        {
            decimal fullPrice = DeliveryPrice;

            _products.ForEach(product => fullPrice += product.GetFullPrice());

            return fullPrice;
        }

        public void Deliver() => Status = OrderStatus.Finished;

        public void Cancel()
        {
            Status = OrderStatus.Cancelled;

            RaiseDomainEvent(new OrderCancelledDomainEvent(Id.Value));
        }

        public string GetName() => $"order-{SerialNumber}";
    }
}
