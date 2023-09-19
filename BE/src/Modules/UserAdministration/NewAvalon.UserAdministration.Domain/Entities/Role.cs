using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class Role : Entity<RoleId>, IAuditableEntity
    {
        private readonly HashSet<Permission> _permissions = new();

        public static readonly Role DealerUser = new(new RoleId(1), nameof(DealerUser), "Dealer user");

        public static readonly Role SuperAdmin = new(new RoleId(2), nameof(SuperAdmin), "Super admin");

        public static readonly Role Client = new(new RoleId(3), nameof(Client), "Client");

        private Role(RoleId id, string name, string description)
            : base(id)
        {
            Name = name;

            Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public ICollection<User> Users { get; private set; }

        public IReadOnlyCollection<Permission> Permissions => _permissions.ToList();

        public bool AddPermission(Permission permission) => _permissions.Add(permission);

        public bool RemovePermission(Permission permission) => _permissions.Remove(permission);
    }
}
