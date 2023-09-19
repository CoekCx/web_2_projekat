using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Exceptions.Files;
using NewAvalon.Storage.Boundary.Files.Commands.DeleteFile;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Commands.DeleteFile
{
    internal sealed class DeleteFileCommandHandler : ICommandHandler<DeleteFileCommand, Unit>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileRepository _fileRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public DeleteFileCommandHandler(IFileStorageService fileStorageService, IFileRepository fileRepository, IStorageUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            File file = await _fileRepository.GetByIdAsync(new FileId(request.FileId), cancellationToken);

            if (file is null)
            {
                throw new FileNotFoundException(request.FileId);
            }

            await _fileStorageService.DeleteAsync(request.FileId, cancellationToken);

            _fileRepository.Remove(file);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
