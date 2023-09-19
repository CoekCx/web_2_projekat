using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace NewAvalon.Catalog.Domain.Exceptions
{
    public sealed class ProductCannotBeDeletedException : UnProcessableEntityException
    {
        public ProductCannotBeDeletedException(Guid id)
            : base("Product cannot be deleted", $"The product with specified identifier {id} cannot be deleted.", Error.ProductCannotBeDeleted)
        {
        }
    }
}
