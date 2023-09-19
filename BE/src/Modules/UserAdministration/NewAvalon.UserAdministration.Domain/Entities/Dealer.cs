using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Enums;
using NewAvalon.UserAdministration.Domain.Events;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using System;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class Dealer : AggregateRoot<UserId>, IAuditableEntity
    {
        public Dealer(UserId id) : base(id)
        {
            Status = DealerStatus.Pending;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dealer"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Dealer()
        {
        }

        public DealerStatus Status { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public void Disapprove()
        {
            if (Status != DealerStatus.Pending)
            {
                throw new DealerAlreadyProcessedException(Id.Value);
            }

            Status = DealerStatus.Disapproved;
        }

        public void Approve()
        {
            if (Status != DealerStatus.Pending)
            {
                throw new DealerAlreadyProcessedException(Id.Value);
            }

            Status = DealerStatus.Approved;

            RaiseDomainEvent(new DealerUserApprovedDomainEvent(Id.Value));
        }
    }
}
