using System;

namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IIsProductUsedRequest
    {
        public Guid ProductId { get; set; }
    }
}
