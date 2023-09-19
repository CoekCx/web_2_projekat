using System;

namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IGetCatalogProductListRequest
    {
        Guid[] ProductIds { get; set; }
    }
}
