using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using System;
using System.Collections.Generic;

namespace NewAvalon.Order.Boundary.Orders.Commands.CreateOrders
{
    public record CreateOrdersCommand(Guid OwnerId, List<AddProductRequest> Products, string Comment, string DeliveryAddress) : ICommand<List<EntityCreatedResponse>>;
}
