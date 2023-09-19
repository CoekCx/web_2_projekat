using NewAvalon.Messaging.Contracts.Products;
using System;

namespace NewAvalon.Order.Business.Contracts.Products
{
    internal sealed class GetCatalogProductListRequest : IGetCatalogProductListRequest
    {
        public Guid[] ProductIds { get; set; }
    }
}
