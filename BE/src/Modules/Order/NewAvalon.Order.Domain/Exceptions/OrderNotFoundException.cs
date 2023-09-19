using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace NewAvalon.Order.Domain.Exceptions
{
    public sealed class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id)
            : base("Order not found", $"The order with specified identifier {id} was not found.", Error.OrderNotFound)
        {
        }
    }
}
