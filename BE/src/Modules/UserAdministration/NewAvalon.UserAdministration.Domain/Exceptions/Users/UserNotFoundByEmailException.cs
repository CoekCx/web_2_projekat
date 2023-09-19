using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;

namespace NewAvalon.UserAdministration.Domain.Exceptions.Users
{
    public sealed class UserNotFoundByEmailException : NotFoundException
    {
        public UserNotFoundByEmailException(string email)
            : base("User not found", $"The user with {email} email was not found.", Error.UserNotFound)
        {
        }
    }
}
