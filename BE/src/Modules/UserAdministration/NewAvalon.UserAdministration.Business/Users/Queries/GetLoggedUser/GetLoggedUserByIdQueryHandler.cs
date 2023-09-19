using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Contracts.Users;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetLoggedUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetLoggedUser
{
    internal sealed class GetLoggedUserByIdQueryHandler : IQueryHandler<GetLoggedUserByIdQuery, UserWithPermissionsResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDealerRepository _dealerRepository;

        public GetLoggedUserByIdQueryHandler(IUserRepository userRepository, IDealerRepository dealerRepository)
        {
            _userRepository = userRepository;
            _dealerRepository = dealerRepository;
        }
        public async Task<UserWithPermissionsResponse> Handle(GetLoggedUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);

            User user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            Dealer dealer = await _dealerRepository.GetByIdAsync(userId, cancellationToken);

            return new UserWithPermissionsResponse(
                user.Id.Value,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Address,
                dealer is not null ? (int)dealer.Status : null,
                user.ProfileImage != null ? new ProfileImageResponse(user.ProfileImage.Id, user.ProfileImage.Url) : null,
                user.Roles.Select(role => new RoleResponse(role.Id.Value, role.Description))
                    .ToList(),
                user.Roles.SelectMany(role =>
                    role.Permissions.Select(permission => new PermissionResponse(permission.Name, permission.Description))).ToList());
        }
    }
}
