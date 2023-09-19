using NewAvalon.Abstractions.Data;
using NewAvalon.Messaging.Contracts.Users;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Business.Users.Consumers.UserDetailsListRequest
{
    public interface IGetUserDetailsListDataRequest : IDataRequest<GetUserDetailsListRequest, List<IUserDetailsResponse>>
    {
    }
}
