using NewAvalon.Domain.Errors;

namespace NewAvalon.Domain.Exceptions
{
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string title, string message, Error error = default)
            : base(title, message, error ?? Error.NotFound)
        {
        }
    }
}
