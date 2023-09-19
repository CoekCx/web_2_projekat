using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace NewAvalon.Order.Domain.Exceptions
{
    public sealed class ProductInvalidQuantityException : ConflictException
    {
        public ProductInvalidQuantityException(Guid id)
            : base("Product invalid quantity", $"The product with specified identifier {id} does not have capacity", Error.ProductInvalidQuantity)
        {
        }
    }
}
