using NewAvalon.Domain.Errors;

namespace NewAvalon.Domain.Exceptions
{
    public abstract class ConflictException : DomainException
    {
        protected ConflictException(string title, string message, Error error = default)
            : base(title, message, error ?? Error.Conflict)
        {
        }
    }
}
