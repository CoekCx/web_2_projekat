using MediatR;
using NewAvalon.Abstractions.Messaging;
using System;

namespace NewAvalon.Order.Boundary.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid UserId, Guid OrderId) : ICommand<Unit>;
}
