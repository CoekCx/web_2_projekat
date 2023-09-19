using Microsoft.EntityFrameworkCore;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Persistence.Extensions;
using System.Collections.Generic;

namespace NewAvalon.Persistence.Relational
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected abstract void OnModelCreatingInternal(ModelBuilder modelBuilder);

        protected sealed override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingInternal(modelBuilder);

            modelBuilder.ApplyUtcDateTimeConverter();

            base.OnModelCreating(modelBuilder);
        }

        protected void AddOutbox(ModelBuilder modelBuilder) => modelBuilder.AddOutbox();

        protected void SeedData<TEntity, T>(ModelBuilder modelBuilder, List<TEntity> data)
            where TEntity : Entity<T>
            where T : class, IEntityId => modelBuilder.SeedData<TEntity, T>(data);

        protected void SeedJoinData<TFirst, TSecond>(
            ModelBuilder modelBuilder,
            params (TFirst First, TSecond Second)[] data)
            where TFirst : class
            where TSecond : class =>
            modelBuilder.HasJoinData(data);
    }
}
