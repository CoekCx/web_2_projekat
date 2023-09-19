using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Persistence.Relational.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewAvalon.Persistence.Extensions
{
    internal static class ModelBuilderExtensions
    {
        private static readonly ValueConverter<DateTime, DateTime> UtcDateTimeValueConverter =
            new(dateTime => dateTime, dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));

        internal static void ApplyUtcDateTimeConverter(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                IEnumerable<IMutableProperty> dateTimeUtcProperties = mutableEntityType
                    .GetProperties()
                    .Where(PropertyIsOfDateTimeTypeAndEndsWithUtc);

                foreach (IMutableProperty mutableProperty in dateTimeUtcProperties)
                {
                    mutableProperty.SetValueConverter(UtcDateTimeValueConverter);
                }
            }
        }

        public static void AddOutbox(this ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

        private static bool PropertyIsOfDateTimeTypeAndEndsWithUtc(IMutableProperty property) =>
            property.ClrType == typeof(DateTime) && property.Name.EndsWith("Utc", StringComparison.Ordinal);

        public static void SeedData<TEntity, T>(this ModelBuilder modelBuilder, List<TEntity> data)
            where TEntity : Entity<T>
            where T : class, IEntityId
            => modelBuilder.Entity<TEntity>()
                .HasData(data);

        public static void HasJoinData<TFirst, TSecond>(
            this ModelBuilder modelBuilder,
            params (TFirst First, TSecond Second)[] data)
            where TFirst : class
            where TSecond : class => modelBuilder.HasJoinData(data.AsEnumerable());

        public static void HasJoinData<TFirst, TSecond>(
            this ModelBuilder modelBuilder,
            IEnumerable<(TFirst First, TSecond Second)> data)
            where TFirst : class
            where TSecond : class
        {
            IMutableEntityType firstEntityType = modelBuilder.Model.FindEntityType(typeof(TFirst));
            IMutableEntityType secondEntityType = modelBuilder.Model.FindEntityType(typeof(TSecond));
            IMutableSkipNavigation firstToSecond = firstEntityType.GetSkipNavigations()
                .Single(n => n.TargetEntityType == secondEntityType);
            IMutableEntityType joinEntityType = firstToSecond.JoinEntityType;
            IMutableProperty firstProperty = firstToSecond.ForeignKey.Properties.Single();
            IMutableProperty secondProperty = firstToSecond.Inverse.ForeignKey.Properties.Single();
            IClrPropertyGetter firstValueGetter = firstToSecond.ForeignKey.PrincipalKey.Properties.Single().GetGetter();
            IClrPropertyGetter secondValueGetter = firstToSecond.Inverse.ForeignKey.PrincipalKey.Properties.Single().GetGetter();
            IEnumerable<object> seedData = data.Select(e => (object)new Dictionary<string, object>
            {
                [firstProperty.Name] = firstValueGetter.GetClrValue(e.First),
                [secondProperty.Name] = secondValueGetter.GetClrValue(e.Second),
            });
            modelBuilder.Entity(joinEntityType.Name).HasData(seedData);
        }
    }
}
