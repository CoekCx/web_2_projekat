using NewAvalon.Domain.Errors;

namespace NewAvalon.Domain.Exceptions
{
    public abstract class BadRequestException : DomainException
    {
        protected BadRequestException(string title, string message, Error error = default) : base(title, message, error ?? Error.BadRequest)
        {
        }
    }
}
