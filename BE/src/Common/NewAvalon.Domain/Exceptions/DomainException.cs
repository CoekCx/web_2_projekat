using NewAvalon.Domain.Errors;
using System;

namespace NewAvalon.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string title, string message, Error error = default)
            : base(message)
        {
            Title = title;
            Error = error ?? Error.Default;
        }

        public string Title { get; }

        public Error Error { get; }
    }
}
