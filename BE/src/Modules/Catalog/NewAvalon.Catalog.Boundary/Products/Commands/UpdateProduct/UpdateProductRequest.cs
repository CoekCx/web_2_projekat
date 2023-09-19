namespace NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductRequest(
        string Name,
        decimal Price,
        decimal Capacity,
        string Description);
}
