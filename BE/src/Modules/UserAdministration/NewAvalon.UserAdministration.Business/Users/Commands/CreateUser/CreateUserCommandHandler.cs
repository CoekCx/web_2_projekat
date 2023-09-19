using Epoche;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.CreateUser
{
    //Should use factory, only for testing like this
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, EntityCreatedResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IDealerRepository dealerRepository,
            IClientRepository clientRepository,
            IRoleRepository roleRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _dealerRepository = dealerRepository;
            _clientRepository = clientRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EntityCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await VerifyRequest(request.Email, request.Username, cancellationToken);

            var user = new User(
                new Domain.EntityIdentifiers.UserId(Guid.NewGuid()),
                request.FirstName,
                request.LastName,
                request.Username,
                request.Email,
                request.DateOfBirth,
                request.Address);

            var password = GeneratePassword(user.Id.Value, request.Password);

            user.UpdatePassword(password);

            _userRepository.Insert(user);

            if (request.Roles == Role.Client.Id.Value)
            {
                user.AddRole(_roleRepository.GetByRole(Role.Client));

                user.AddRole(Role.Client);
                var client = new Client(user.Id);
                _clientRepository.Insert(client);
            }
            else if (request.Roles == Role.DealerUser.Id.Value)
            {
                user.AddRole(_roleRepository.GetByRole(Role.DealerUser));
                var dealer = new Dealer(user.Id);
                _dealerRepository.Insert(dealer);
            }
            else
            {
                throw new ArgumentException();
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new EntityCreatedResponse(user.Id.Value);
        }

        private string GeneratePassword(Guid userId, string password)
        {
            var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));

            return Encoding.UTF8.GetString(keccak);
        }

        private async Task VerifyRequest(string email, string userName, CancellationToken cancellationToken)
        {
            bool isEmailTaken = await _userRepository.IsEmailTakenAsync(email, cancellationToken);

            if (isEmailTaken)
            {
                throw new UserWithEmailAlreadyExistsException();
            }

            bool isUserNameTaken = await _userRepository.IsUserNameTakenAsync(userName, cancellationToken);

            if (isUserNameTaken)
            {
                throw new UserWithUserNameAlreadyExists();
            }
        }
    }
}
