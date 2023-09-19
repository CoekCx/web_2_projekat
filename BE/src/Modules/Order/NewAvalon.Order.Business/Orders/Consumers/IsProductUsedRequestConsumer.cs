using MassTransit;
using NewAvalon.Messaging.Contracts.Products;
using NewAvalon.Order.Domain.Repositories;
using System.Threading.Tasks;

namespace NewAvalon.Order.Business.Orders.Consumers
{
    public sealed class IsProductUsedRequestConsumer : IConsumer<IIsProductUsedRequest>
    {
        private readonly IProductRepository _productRepository;

        public IsProductUsedRequestConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<IIsProductUsedRequest> context)
        {
            (bool isUsed, bool isCurrentlyInUse) = await _productRepository.ExistsByCatalogIdAsync(context.Message.ProductId, context.CancellationToken);

            var response = new IsProductUsedResponse
            {
                IsUsed = isUsed,
                IsCurrentlyInUse = isCurrentlyInUse
            };

            await context.RespondAsync<IIsProductUsedResponse>(response);
        }

        private sealed class IsProductUsedResponse : IIsProductUsedResponse
        {
            public bool IsUsed { get; set; }

            public bool IsCurrentlyInUse { get; set; }
        }
    }
}
