namespace NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct
{
    public sealed record CreateProductRequest(
        string Name,
        decimal Price,
        decimal Capacity,
        string Description);
}
