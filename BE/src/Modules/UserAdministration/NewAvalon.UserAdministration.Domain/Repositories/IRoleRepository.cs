using NewAvalon.UserAdministration.Domain.Entities;

namespace NewAvalon.UserAdministration.Domain.Repositories
{
    public interface IRoleRepository
    {
        Role GetByRole(Role role);
    }
}
