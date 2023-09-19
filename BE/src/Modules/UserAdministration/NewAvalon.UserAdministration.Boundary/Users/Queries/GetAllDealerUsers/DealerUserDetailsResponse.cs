using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Queries.GetAllDealerUsers
{
    public sealed record DealerUserDetailsResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        int Status);
}
