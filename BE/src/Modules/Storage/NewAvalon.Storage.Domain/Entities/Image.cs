using NewAvalon.Domain.Abstractions;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using System;

namespace NewAvalon.Storage.Domain.Entities
{
    public sealed class Image : Entity<ImageId>, IAuditableEntity
    {
        public Image(ImageId id, string url)
            : base(id) =>
            Url = url;

        public string Url { get; set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
