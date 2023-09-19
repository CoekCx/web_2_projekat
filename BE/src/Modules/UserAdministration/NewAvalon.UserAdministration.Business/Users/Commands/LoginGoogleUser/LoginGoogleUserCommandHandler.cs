using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Abstractions.Services;
using NewAvalon.UserAdministration.Boundary.Users.Commands.LoginGoogleUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.LoginGoogleUser
{
    internal sealed class LoginGoogleUserCommandHandler : ICommandHandler<LoginGoogleUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ISystemTime _systemTime;
        private readonly IDealerRepository _dealerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public LoginGoogleUserCommandHandler(
            IUserRepository userRepository,
            IJwtProvider jwtProvider,
            ISystemTime systemTime,
            IDealerRepository dealerRepository,
            IClientRepository clientRepository,
            IRoleRepository roleRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _systemTime = systemTime;
            _dealerRepository = dealerRepository;
            _clientRepository = clientRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(LoginGoogleUserCommand request, CancellationToken cancellationToken)
        {
            var googleUserDetails = _jwtProvider.GetUserDetailsFromGoogleJwt(request.Token);

            User user = await _userRepository.GetByEmailAsync(googleUserDetails.Email, cancellationToken);

            if (user != null)
            {
                return _jwtProvider.Generate(new GenerateTokenRequest(googleUserDetails.Email, user.Id.Value));

            }

            var newUser = new User(
                new Domain.EntityIdentifiers.UserId(Guid.NewGuid()),
                googleUserDetails.FirstName,
                googleUserDetails.LastName,
                googleUserDetails.Email,
                googleUserDetails.Email,
                _systemTime.UtcNow,
                null);

            _userRepository.Insert(newUser);

            if (request.Role == Role.Client.Id.Value)
            {
                newUser.AddRole(_roleRepository.GetByRole(Role.Client));

                newUser.AddRole(Role.Client);
                var client = new Client(newUser.Id);
                _clientRepository.Insert(client);
            }
            else if (request.Role == Role.DealerUser.Id.Value)
            {
                newUser.AddRole(_roleRepository.GetByRole(Role.DealerUser));
                var dealer = new Dealer(newUser.Id);
                _dealerRepository.Insert(dealer);
            }
            else
            {
                throw new ArgumentException();
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _jwtProvider.Generate(new GenerateTokenRequest(googleUserDetails.Email, newUser.Id.Value));
        }
    }
}
