using NewAvalon.Messaging.Contracts.Users;

namespace NewAvalon.UserAdministration.Business.Contracts.Users
{
    internal sealed class UserDetailsListResponse : IUserDetailsListResponse
    {
        public IUserDetailsResponse[] Users { get; set; }
    }
}
