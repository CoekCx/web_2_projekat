using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class Client : Entity<UserId>, IAuditableEntity
    {
        public Client(UserId id) : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Client()
        {
        }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
