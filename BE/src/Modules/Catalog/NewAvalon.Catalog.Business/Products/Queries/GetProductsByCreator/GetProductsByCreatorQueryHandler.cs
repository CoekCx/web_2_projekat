using NewAvalon.Abstractions.Messaging;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProductsByCreator;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Queries.GetProductsByCreator
{
    internal sealed class GetProductsByCreatorQueryHandler : IQueryHandler<GetProductsByCreatorQuery, PagedList<CatalogProductDetailsResponse>>
    {
        private readonly IGetAllProductsByCreatorIdDataRequest _getAllProductsByCreatorIdDataRequest;

        public GetProductsByCreatorQueryHandler(IGetAllProductsByCreatorIdDataRequest getAllProductsByCreatorIdDataRequest)
        {
            _getAllProductsByCreatorIdDataRequest = getAllProductsByCreatorIdDataRequest;
        }

        public async Task<PagedList<CatalogProductDetailsResponse>> Handle(GetProductsByCreatorQuery request, CancellationToken cancellationToken) =>
            await _getAllProductsByCreatorIdDataRequest.GetAsync((request.CreatorId, request.Page, request.ItemsPerPage), cancellationToken);
    }
}
