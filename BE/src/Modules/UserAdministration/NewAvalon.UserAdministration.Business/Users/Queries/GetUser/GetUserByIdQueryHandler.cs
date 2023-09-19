using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetUser
{
    internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailsResponse>
    {
        private readonly IGetUserByIdDataRequest _getUserByIdDataRequest;

        public GetUserByIdQueryHandler(IGetUserByIdDataRequest getUserByIdDataRequest)
        {
            _getUserByIdDataRequest = getUserByIdDataRequest;
        }

        public async Task<UserDetailsResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
            await _getUserByIdDataRequest.GetAsync(new UserId(request.UserId), cancellationToken);
    }
}
