using NewAvalon.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace NewAvalon.Catalog.Domain.ValueObjects
{
    public sealed class ProductImage : ValueObject
    {
        public static readonly ProductImage Empty = new(Guid.Empty, string.Empty);

        private ProductImage(Guid id, string url) => (Id, Url) = (id, url);

        public Guid Id { get; private set; }

        public string Url { get; private set; }

        public static ProductImage Create(Guid? id, string url)
        {
            if (id is null || id == Guid.Empty || string.IsNullOrWhiteSpace(url))
            {
                return Empty;
            }

            return new ProductImage(id.Value, url);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Url;
        }
    }
}
