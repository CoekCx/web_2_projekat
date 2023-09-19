using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;

namespace NewAvalon.UserAdministration.Domain.Exceptions.Users
{
    public sealed class UserWithEmailAlreadyExistsException : ConflictException
    {
        public UserWithEmailAlreadyExistsException()
            : base("User already exists", "The user with the specified email already exists.", Error.UserAlreadyExists)
        {
        }
    }
}
