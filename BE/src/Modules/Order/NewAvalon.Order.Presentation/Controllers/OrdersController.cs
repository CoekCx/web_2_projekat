using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Authorization;
using NewAvalon.Authorization.Attributes;
using NewAvalon.Authorization.Extensions;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Commands.CreateOrders;
using NewAvalon.Order.Boundary.Orders.Commands.DeleteOrder;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Boundary.Orders.Queries.GetOrder;
using NewAvalon.Order.Boundary.Orders.Queries.GetShippingOrders;
using NewAvalon.Order.Presentation.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Presentation.Controllers
{
    /// <summary>
    /// Represents the orders controller.
    /// </summary>
    public sealed class OrdersController : OrderController
    {
        /// <summary>
        /// Create new orders.
        /// </summary>
        /// <param name="request">The create orders request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpPost]
        [Authorize]
        [HasPermission(Permissions.OrderCreate)]
        [ProducesResponseType(typeof(List<EntityCreatedResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrders(
            CreateOrdersRequest request,
            CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = request.Adapt<CreateOrdersCommand>() with
            {
                OwnerId = userId
            };

            List<EntityCreatedResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet]
        [Authorize]
        [HasPermission(Permissions.OrderRead)]
        [ProducesResponseType(typeof(PagedList<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllOrders(
            int page,
            int itemsPerPage,
            CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = new GetAllOrdersQuery(
                userId,
                page,
                itemsPerPage);

            PagedList<OrderDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Get order by specified identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("{orderId:guid}")]
        [Authorize]
        [HasPermission(Permissions.OrderRead)]
        [ProducesResponseType(typeof(PagedList<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = new GetOrderByIdQuery(orderId, userId);

            OrderDetailsResponse response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets shipping orders.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("shipping")]
        [Authorize]
        [HasPermission(Permissions.OrderRead)]
        [ProducesResponseType(typeof(PagedList<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShippingOrders(
            int page,
            int itemsPerPage,
            CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = new GetShippingOrdersQuery(
                userId,
                page,
                itemsPerPage);

            PagedList<OrderDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets shipping orders.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpDelete("{orderId:guid}")]
        [Authorize]
        [HasPermission(Permissions.OrderDelete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid orderId, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var command = new DeleteOrderCommand(userId, orderId);

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
