using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IProductImageChangedEvent : IEvent
    {
        Guid OldImageId { get; set; }
    }
}
