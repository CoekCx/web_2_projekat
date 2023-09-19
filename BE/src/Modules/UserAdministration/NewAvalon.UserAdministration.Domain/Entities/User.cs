using NewAvalon.Domain.Abstractions;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Events;
using NewAvalon.UserAdministration.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.UserAdministration.Domain.Entities
{
    public sealed class User : AggregateRoot<UserId>, IAuditableEntity
    {
        private readonly HashSet<Role> _roles = new();

        public User(UserId id, string firstName, string lastName, string userName, string email, DateTime dateOfBirth, string address)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private User()
        {
        }

        public Guid? IdentityProviderId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UserName { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public string Address { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public ProfileImage ProfileImage { get; private set; }

        public IReadOnlyCollection<Role> Roles => _roles.ToList();

        public bool AddRole(Role role) => _roles.Add(role);

        public bool RemoveRole(Role role) => _roles.Remove(role);

        public void Update(string firstName, string lastName, string userName, string password, DateTime dateOfBirth, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            DateOfBirth = dateOfBirth;
            Address = address;

            if (!string.IsNullOrWhiteSpace(password))
            {
                Password = password;
            }
        }

        public void ChangeProfileImage(ProfileImage profileImage)
        {
            if (ProfileImage == profileImage)
            {
                return;
            }

            ProfileImage ??= ProfileImage.Empty;

            if (ProfileImage != ProfileImage.Empty)
            {
                RaiseDomainEvent(new UserProfileImageChangedDomainEvent(ProfileImage.Id));
            }

            ProfileImage = profileImage;
        }

        public void RemoveProfileImage() => ProfileImage = ProfileImage.Empty;

        public void UpdatePassword(string password) => Password = password;
    }
}
