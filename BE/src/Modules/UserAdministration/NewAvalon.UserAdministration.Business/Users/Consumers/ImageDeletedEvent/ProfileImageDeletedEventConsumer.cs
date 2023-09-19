using MassTransit;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Repositories;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Consumers.ImageDeletedEvent
{
    public sealed class ProfileImageDeletedEventConsumer : IConsumer<IImageDeletedEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public ProfileImageDeletedEventConsumer(IUserRepository userRepository, IUserAdministrationUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IImageDeletedEvent> context)
        {
            User user = await _userRepository.GetByImageIdAsync(context.Message.ImageId, context.CancellationToken);

            if (user is null)
            {
                return;
            }

            user.RemoveProfileImage();

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
