using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : ICommand<string>;
}
