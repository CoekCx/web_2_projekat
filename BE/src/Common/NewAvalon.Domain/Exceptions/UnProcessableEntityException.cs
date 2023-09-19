using NewAvalon.Domain.Errors;

namespace NewAvalon.Domain.Exceptions
{
    public abstract class UnProcessableEntityException : DomainException
    {
        protected UnProcessableEntityException(string title, string message, Error error = default)
            : base(title, message, error ?? Error.UnProcessableEntity)
        {
        }
    }
}
