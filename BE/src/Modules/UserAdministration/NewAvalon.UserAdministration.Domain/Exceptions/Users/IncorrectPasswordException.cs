using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;

namespace NewAvalon.UserAdministration.Domain.Exceptions.Users
{
    public sealed class IncorrectPasswordException : BadRequestException
    {
        public IncorrectPasswordException()
            : base("Incorrect password", "Password is incorrect.", Error.DealerAlreadyProcessed)
        {
        }
    }
}
