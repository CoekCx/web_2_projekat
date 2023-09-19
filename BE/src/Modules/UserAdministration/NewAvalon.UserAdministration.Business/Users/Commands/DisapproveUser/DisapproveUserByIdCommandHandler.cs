using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.DisapproveUser;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.DisapproveUser
{
    internal sealed class DisapproveUserByIdCommandHandler : ICommandHandler<DisapproveUserByIdCommand, Unit>
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public DisapproveUserByIdCommandHandler(
            IDealerRepository userRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _dealerRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DisapproveUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _dealerRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            user.Disapprove();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
