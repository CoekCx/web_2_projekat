using NewAvalon.Abstractions.Data;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;

namespace NewAvalon.UserAdministration.Business.Users.Queries.GetUser
{
    public interface IGetUserByIdDataRequest : IDataRequest<UserId, UserDetailsResponse>
    {
    }
}
