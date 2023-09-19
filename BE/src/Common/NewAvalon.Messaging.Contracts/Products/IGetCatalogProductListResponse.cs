namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IGetCatalogProductListResponse
    {
        IGetCatalogProductResponse[] Products { get; set; }
    }
}
