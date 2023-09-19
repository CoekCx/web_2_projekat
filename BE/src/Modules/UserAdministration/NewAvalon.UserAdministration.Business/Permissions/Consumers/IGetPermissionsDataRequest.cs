using NewAvalon.Abstractions.Data;

namespace NewAvalon.UserAdministration.Business.Permissions.Consumers
{
    public interface IGetPermissionsDataRequest : IDataRequest<GetPermissionsRequest, string[]>
    {
    }
}
