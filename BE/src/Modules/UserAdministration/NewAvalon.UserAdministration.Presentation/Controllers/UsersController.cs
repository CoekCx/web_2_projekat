using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Authorization;
using NewAvalon.Authorization.Attributes;
using NewAvalon.Authorization.Extensions;
using NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.LoginGoogleUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.LoginUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUserImage;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetLoggedUser;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Presentation.Controllers
{
    /// <summary>
    /// Represents the User controller.
    /// </summary>
    public sealed class UsersController : UserAdministrationController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController() { }

        /// <summary>
        /// Creates a new user based on the specified request.
        /// </summary>
        /// <param name="request">The create user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The identifier of the newly created user.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            CreateUserCommand command = request.Adapt<CreateUserCommand>();

            EntityCreatedResponse response = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetUser), new { userId = response.EntityId }, response.EntityId);
        }

        /// <summary>
        /// Login user based on the specified request.
        /// </summary>
        /// <param name="request">The login user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The token of the newly logged user.</returns>
        [HttpPut("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            LoginUserCommand command = request.Adapt<LoginUserCommand>();

            string response = await Sender.Send(command, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Login user based on the specified request.
        /// </summary>
        /// <param name="request">The login user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The token of the newly logged user.</returns>
        [HttpPut("signin-google")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginGoogleUser([FromBody] LoginGoogleUserRequest request, CancellationToken cancellationToken)
        {
            LoginGoogleUserCommand command = request.Adapt<LoginGoogleUserCommand>();

            string response = await Sender.Send(command, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("{userId:guid}", Name = nameof(GetUser))]
        [Authorize]
        [HasPermission(Permissions.UserRead)]
        [ProducesResponseType(typeof(UserDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(userId);

            UserDetailsResponse response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="request">The login user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The token of the newly logged user.</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            UpdateUserCommand command = request.Adapt<UpdateUserCommand>() with
            {
                Id = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Gets the logged user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("me", Name = nameof(GetLoggedUser))]
        [Authorize]
        [ProducesResponseType(typeof(UserWithPermissionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetLoggedUser(CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = new GetLoggedUserByIdQuery(userId);

            UserWithPermissionsResponse response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Updates the currently logged in user's profile image.
        /// </summary>
        /// <param name="request">The update user image request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>200 - OK.</returns>
        [HttpPut("profile-image")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserImage(
            [FromBody] UpdateUserImageRequest request,
            CancellationToken cancellationToken)
        {
            UpdateUserImageCommand command = request.Adapt<UpdateUserImageCommand>() with
            {
                Id = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

            await Sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}
