using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Authorization;
using NewAvalon.Authorization.Attributes;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Commands.ApproveUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.DisapproveUser;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetAllDealerUsers;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetPendingUsers;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Presentation.Controllers
{
    /// <summary>
    /// Represents the dealers controller.
    /// </summary>
    [Route("api/administration/dealer")]
    public sealed class DealersController : UserAdministrationController
    {
        /// <summary>
        /// Gets the users that are not approved.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("pending")]
        [Authorize]
        [HasPermission(Permissions.DealerRead)]
        [ProducesResponseType(typeof(PagedList<UserDetailsResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPendingUsers(
            int page,
            int itemsPerPage,
            CancellationToken cancellationToken)
        {
            var query = new GetPendingUsersQuery(page, itemsPerPage);

            PagedList<UserDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Approve user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpPut("approve/{userId:guid}")]
        [Authorize]
        [HasPermission(Permissions.DealerUpdate)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> ApproveUser(Guid userId, CancellationToken cancellationToken)
        {
            var command = new ApproveUserByIdCommand(userId);

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Disapprove user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpPut("disapprove/{userId:guid}")]
        [Authorize]
        [HasPermission(Permissions.DealerUpdate)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DisapproveUser(Guid userId, CancellationToken cancellationToken)
        {
            var command = new DisapproveUserByIdCommand(userId);

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Gets all the dealer users.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet]
        [Authorize]
        [HasPermission(Permissions.DealerRead)]
        [ProducesResponseType(typeof(PagedList<DealerUserDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllDealerUsers(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            var query = new GetAllDealerUsersQuery(page, itemsPerPage);

            PagedList<DealerUserDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }
    }
}
