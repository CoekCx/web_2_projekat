using System;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUserImage
{
    public sealed record UpdateUserImageRequest(Guid? ImageId);
}
