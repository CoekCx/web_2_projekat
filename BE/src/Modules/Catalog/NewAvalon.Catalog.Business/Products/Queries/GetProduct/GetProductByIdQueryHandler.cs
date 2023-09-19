using NewAvalon.Abstractions.Messaging;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProduct
{
    internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, CatalogProductDetailsResponse>
    {
        private readonly IGetProductByIdDataRequest _getProductByIdDataRequest;

        public GetProductByIdQueryHandler(IGetProductByIdDataRequest getProductByIdDataRequest)
        {
            _getProductByIdDataRequest = getProductByIdDataRequest;
        }

        public async Task<CatalogProductDetailsResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
            await _getProductByIdDataRequest.GetAsync(new ProductId(request.ProductId), cancellationToken);
    }
}
