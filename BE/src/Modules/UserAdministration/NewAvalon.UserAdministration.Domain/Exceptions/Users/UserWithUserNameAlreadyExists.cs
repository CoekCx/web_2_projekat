using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;

namespace NewAvalon.UserAdministration.Domain.Exceptions.Users
{
    public sealed class UserWithUserNameAlreadyExists : ConflictException
    {
        public UserWithUserNameAlreadyExists()
            : base("User already exists", "The user with the specified username already exists.", Error.UserAlreadyExists)
        {
        }
    }
}
