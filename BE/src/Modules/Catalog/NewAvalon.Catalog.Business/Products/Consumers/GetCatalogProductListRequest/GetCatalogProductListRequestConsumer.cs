using MassTransit;
using NewAvalon.Catalog.Domain.Entities;
using NewAvalon.Catalog.Domain.EntityIdentifiers;
using NewAvalon.Catalog.Domain.Repositories;
using NewAvalon.Messaging.Contracts.Products;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Business.Products.Consumers.GetCatalogProductListRequest
{
    public sealed class GetCatalogProductListRequestConsumer : IConsumer<IGetCatalogProductListRequest>
    {
        private readonly IProductRepository _productRepository;

        public GetCatalogProductListRequestConsumer(IProductRepository productRepository) =>
            _productRepository = productRepository;

        public async Task Consume(ConsumeContext<IGetCatalogProductListRequest> context)
        {
            var productIds = context.Message.ProductIds.Select(x => new ProductId(x)).ToArray();

            Product[] products = await _productRepository.GetByIdsAsync(productIds, context.CancellationToken);

            var response = new GetCatalogProductListResponse
            {
                Products = products.Select(x => new GetCatalogProductResponse
                {
                    Id = x.Id.Value,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Capacity,
                    Description = x.Description,
                    CreatorId = x.CreatorId
                }).Cast<IGetCatalogProductResponse>().ToArray()
            };

            await context.RespondAsync<IGetCatalogProductListResponse>(response);
        }

        private sealed class GetCatalogProductListResponse : IGetCatalogProductListResponse
        {
            public IGetCatalogProductResponse[] Products { get; set; }
        }

        private sealed class GetCatalogProductResponse : IGetCatalogProductResponse
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public decimal Price { get; set; }

            public decimal Quantity { get; set; }

            public string Description { get; set; }

            public Guid CreatorId { get; set; }
        }
    }
}
