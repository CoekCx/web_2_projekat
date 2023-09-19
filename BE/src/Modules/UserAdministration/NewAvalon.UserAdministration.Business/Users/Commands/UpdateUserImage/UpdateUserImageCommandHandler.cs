using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Exceptions.Images;
using NewAvalon.Messaging.Contracts.Images;
using NewAvalon.UserAdministration.Boundary.Users.Commands.UpdateUserImage;
using NewAvalon.UserAdministration.Business.Abstractions;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.EntityIdentifiers;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using NewAvalon.UserAdministration.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Commands.UpdateUserImage
{
    internal sealed class UpdateUserImageCommandHandler : ICommandHandler<UpdateUserImageCommand, Unit>
    {
        private readonly IImageService _imageService;
        private readonly IUserRepository _userRepository;
        private readonly IUserAdministrationUnitOfWork _unitOfWork;

        public UpdateUserImageCommandHandler(
            IImageService imageService,
            IUserRepository userRepository,
            IUserAdministrationUnitOfWork unitOfWork)
        {
            _imageService = imageService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            IImageResponse imageResponse = null;
            if (request.ImageId.HasValue && !(imageResponse = await _imageService.GetByIdAsync(request.ImageId.Value, cancellationToken)).Exists)
            {
                throw new ImageNotFoundException(request.ImageId.Value);
            }

            User user = await _userRepository.GetByIdAsync(new UserId(request.Id), cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            var profileImage = ProfileImage.Create(imageResponse?.ImageId, imageResponse?.ImageUrl);

            user.ChangeProfileImage(profileImage);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
