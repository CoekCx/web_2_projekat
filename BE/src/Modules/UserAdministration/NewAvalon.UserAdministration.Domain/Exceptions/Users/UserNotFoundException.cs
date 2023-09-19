using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace NewAvalon.UserAdministration.Domain.Exceptions.Users
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId)
            : base("User not found", $"The user with {userId} identifier was not found.", Error.UserNotFound)
        {
        }
    }
}
