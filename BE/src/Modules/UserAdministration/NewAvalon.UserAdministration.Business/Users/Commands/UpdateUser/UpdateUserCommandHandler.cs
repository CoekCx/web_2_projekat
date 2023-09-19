using Epoche;
using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUser;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(new UserId(request.Id), cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            user.Update(
                request.FirstName,
                request.LastName,
                request.Username,
                request.Password is null
                    ? null
                    : GeneratePassword(user.Id.Value, request.Password),
                request.DateOfBirth,
                request.Address);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private string GeneratePassword(Guid userId, string password)
        {
            var keccak = Keccak256.ComputeHash(Encoding.UTF8.GetBytes(userId + password));

            return Encoding.UTF8.GetString(keccak);
        }
    }
}
