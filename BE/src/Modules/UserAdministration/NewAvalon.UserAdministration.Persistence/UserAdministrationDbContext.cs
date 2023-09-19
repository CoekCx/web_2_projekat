using Epoche;
using Microsoft.EntityFrameworkCore;
using NewAvalon.Authorization;
using NewAvalon.Infrastructure.Extensions;
using NewAvalon.Persistence.Relational;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Persistence
{
    public sealed class UserAdministrationDbContext : BaseDbContext, IUserAdministrationUnitOfWork
    {
        public UserAdministrationDbContext(DbContextOptions options)
             : base(options)
        {
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.CommitTransactionAsync(cancellationToken);

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
            await Database.RollbackTransactionAsync(cancellationToken);

        protected override void OnModelCreatingInternal(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            AddOutbox(modelBuilder);

            SeedUserAdministrationData(modelBuilder);
        }

        private void SeedUserAdministrationData(ModelBuilder modelBuilder)
        {
            SeedData<Role, RoleId>(modelBuilder, new List<Role>()
            {
                Role.DealerUser,
                Role.SuperAdmin,
                Role.Client
            });

            var permissions = Enum.GetValues(typeof(Permissions))
                .Cast<Permissions>()
                .Select(p => new Permission(new PermissionId((int)p), p.ToString(), p.Description()))
                .ToList();

            SeedData<Permission, PermissionId>(modelBuilder, permissions);

            SeedJoinData(
                modelBuilder,
                (permissions.First(p => p.Id.Value == (int)Permissions.UserRead), Role.SuperAdmin),
                (permissions.First(p => p.Id.Value == (int)Permissions.OrderRead), Role.SuperAdmin),
                (permissions.First(p => p.Id.Value == (int)Permissions.OrderRead), Role.DealerUser),
                (permissions.First(p => p.Id.Value == (int)Permissions.OrderRead), Role.Client),
                (permissions.First(p => p.Id.Value == (int)Permissions.OrderCreate), Role.Client),
                (permissions.First(p => p.Id.Value == (int)Permissions.ProductCreate), Role.DealerUser),
                (permissions.First(p => p.Id.Value == (int)Permissions.ProductUpdate), Role.DealerUser),
                (permissions.First(p => p.Id.Value == (int)Permissions.ProductDelete), Role.DealerUser),
                (permissions.First(p => p.Id.Value == (int)Permissions.DealerRead), Role.SuperAdmin),
                (permissions.First(p => p.Id.Value == (int)Permissions.DealerUpdate), Role.SuperAdmin),
                (permissions.First(p => p.Id.Value == (int)Permissions.OrderDelete), Role.Client));

            var admin = new User(
                new UserId(Guid.NewGuid()),
                "Admin",
                "Admin",
                "Admin",
                "admin@admin.com",
                DateTime.MinValue,
                "Admin address");


            admin.UpdatePassword(GeneratePassword(admin.Id.Value, "123"));

            SeedData<User, UserId>(modelBuilder, new List<User>
            {
                admin
            });

            SeedJoinData(modelBuilder, (admin, Role.SuperAdmin));
        }

        private string GeneratePassword(Guid userId, string password)
        {
            var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));

            return Encoding.UTF8.GetString(keccak);
        }
    }
}
