using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace NewAvalon.UserAdministration.Domain.Exceptions.Users
{
    public sealed class DealerAlreadyProcessedException : UnProcessableEntityException
    {
        public DealerAlreadyProcessedException(Guid userId)
            : base("Dealer already processed", $"The user with {userId} identifier was already processed.", Error.DealerAlreadyProcessed)
        {
        }
    }
}
