using NewAvalon.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Domain.ValueObjects
{
    public sealed class ProfileImage : ValueObject
    {
        public static readonly ProfileImage Empty = new(Guid.Empty, string.Empty);

        private ProfileImage(Guid id, string url) => (Id, Url) = (id, url);

        public Guid Id { get; private set; }

        public string Url { get; private set; }

        public static ProfileImage Create(Guid? id, string url)
        {
            if (id is null || id == Guid.Empty || string.IsNullOrWhiteSpace(url))
            {
                return Empty;
            }

            return new ProfileImage(id.Value, url);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Url;
        }
    }
}
