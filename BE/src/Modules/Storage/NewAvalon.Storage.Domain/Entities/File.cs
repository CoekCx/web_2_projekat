using NewAvalon.Domain.Abstractions;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using System;

namespace NewAvalon.Storage.Domain.Entities
{
    public sealed class File : Entity<FileId>, IAuditableEntity
    {
        public File(FileId id, string url, string name, string extension, decimal size)
            : base(id)
        {
            Url = url;
            Name = name;
            Extension = extension;
            Size = size;
        }

        public string Url { get; private set; }

        public string Name { get; private set; }

        public string Extension { get; private set; }

        public decimal Size { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
