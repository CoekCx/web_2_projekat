using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Authorization;
using NewAvalon.Authorization.Attributes;
using NewAvalon.Authorization.Extensions;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct;
using NewAvalon.Catalog.Boundary.Products.Commands.DeleteProduct;
using NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct;
using NewAvalon.Catalog.Boundary.Products.Commands.UpdateProductImage;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProducts;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProductsByCreator;
using NewAvalon.Catalog.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Presentation.Controllers
{
    /// <summary>
    /// Represents the dealers controller.
    /// </summary>
    [Route("api/catalog/product")]
    public sealed class ProductsController : CatalogController
    {
        /// <summary>
        /// Creates a new product based on the specified request.
        /// </summary>
        /// <param name="request">The create product request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The identifier of the newly created product.</returns>
        [HttpPost]
        [Authorize]
        [HasPermission(Permissions.ProductCreate)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>() with
            {
                CreatorId = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

            EntityCreatedResponse response = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetProduct), new { productId = response.EntityId }, response.EntityId);
        }

        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="productId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("{productId:guid}", Name = nameof(GetProduct))]
        [Authorize]
        [ProducesResponseType(typeof(CatalogProductDetailsResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(productId);

            CatalogProductDetailsResponse response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets all product.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products.</returns>
        [HttpGet("creator")]
        [Authorize]
        [ProducesResponseType(typeof(PagedList<CatalogProductDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCreator(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            var creatorId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = new GetProductsByCreatorQuery(creatorId, page, itemsPerPage);

            PagedList<CatalogProductDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets all product by creator identifier.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PagedList<CatalogProductDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts(bool onlyActive, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery(onlyActive, page, itemsPerPage);

            PagedList<CatalogProductDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Update product with specified identifier.
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <param name="request">Update product request</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products.</returns>
        [HttpPut("{productId:guid}")]
        [Authorize]
        [HasPermission(Permissions.ProductUpdate)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateProductCommand>() with
            {
                ProductId = productId,
                CreatorId = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Delete the product with the specified identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpDelete("{productId:guid}")]
        [Authorize]
        [HasPermission(Permissions.ProductDelete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid productId, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(productId, Guid.Parse(HttpContext.User.GetUserIdentityId()));

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Updates the currently product image.
        /// </summary>
        /// <param name="request">The update product image request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>200 - OK.</returns>
        [HttpPut("product-image")]
        [Authorize]
        [HasPermission(Permissions.ProductUpdate)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserImage(
            [FromBody] UpdateProductImageRequest request,
            CancellationToken cancellationToken)
        {
            UpdateProductImageCommand command = request.Adapt<UpdateProductImageCommand>() with
            {
                UserId = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

            await Sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}
