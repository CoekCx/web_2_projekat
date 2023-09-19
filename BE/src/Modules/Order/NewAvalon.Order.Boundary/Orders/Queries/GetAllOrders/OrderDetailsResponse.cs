using System;
using System.Collections.Generic;

namespace NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders
{
    public sealed record OrderDetailsResponse(
        Guid Id,
        OrderUserDetailsResponse OwnerId,
        OrderUserDetailsResponse DealerId,
        string Name,
        string Comment,
        string DeliveryAddress,
        int Status,
        List<ProductDetailsResponse> ProductDetailsResponses,
        decimal DeliveryPrice,
        decimal FullPrice,
        DateTime DeliveryOnUtc);

    public sealed record ProductDetailsResponse(
        Guid Id,
        Guid OrderId,
        Guid CatalogProductId,
        decimal Quantity,
        decimal Price,
        decimal FullPrice,
        string Name);

    public sealed record OrderUserDetailsResponse(Guid Id, string FirstName, string LastName, string Username);

}
