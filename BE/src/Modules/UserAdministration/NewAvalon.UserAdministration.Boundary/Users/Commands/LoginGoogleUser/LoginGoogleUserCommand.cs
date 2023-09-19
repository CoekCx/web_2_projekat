using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.UserAdministration.Boundary.Users.Commands.LoginGoogleUser
{
    public record LoginGoogleUserCommand(int Role, string Token) : ICommand<string>;
}
