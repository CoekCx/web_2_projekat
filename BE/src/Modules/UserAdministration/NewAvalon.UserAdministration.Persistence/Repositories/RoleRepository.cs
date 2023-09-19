using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Repositories;

namespace NewAvalon.UserAdministration.Persistence.Repositories
{
    internal sealed class RoleRepository : IRoleRepository
    {
        private readonly UserAdministrationDbContext _dbContext;

        public RoleRepository(UserAdministrationDbContext dbContext) => _dbContext = dbContext;

        public Role GetByRole(Role role) => _dbContext.Set<Role>().Attach(role).Entity;
    }
}
