using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.UserAdministration.Boundary.Users.Commands.ApproveUser;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.ApproveUser
{
    internal sealed class ApproveUserByIdCommandHandler : ICommandHandler<ApproveUserByIdCommand, Unit>
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public ApproveUserByIdCommandHandler(
            IDealerRepository dealerRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _dealerRepository = dealerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ApproveUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _dealerRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            user.Approve();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
