using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class Permission : Entity<PermissionId>
    {
        public Permission(PermissionId id, string name, string description)
            : base(id)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ICollection<Role> Roles { get; private set; }
    }
}
