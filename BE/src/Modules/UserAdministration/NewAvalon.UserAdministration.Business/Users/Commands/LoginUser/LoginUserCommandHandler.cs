using Epoche;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Abstractions.Services;
using NewAvalon.UserAdministration.Boundary.Users.Commands.LoginUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.LoginUser
{
    internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundByEmailException(request.Email);
            }

            if (user.Password != GeneratePassword(user.Id.Value, request.Password))
            {
                throw new IncorrectPasswordException();
            }

            string token = _jwtProvider.Generate(new GenerateTokenRequest(user.Email, user.Id.Value));

            return token;
        }

        private string GeneratePassword(Guid userId, string password)
        {
            var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));

            return Encoding.UTF8.GetString(keccak);
        }
    }
}
