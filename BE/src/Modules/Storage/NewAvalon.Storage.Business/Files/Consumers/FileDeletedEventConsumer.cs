using MassTransit;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Consumers
{
    public class FileDeletedEventConsumer : IConsumer<IFileDeletedEvent>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileRepository _fileRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public FileDeletedEventConsumer(IFileStorageService fileStorageService, IFileRepository fileRepository, IStorageUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IFileDeletedEvent> context)
        {
            await _fileStorageService.DeleteAsync(context.Message.FileId, context.CancellationToken);

            File file = await _fileRepository.GetByIdAsync(new FileId(context.Message.FileId), context.CancellationToken);

            if (file is null)
            {
                return;
            }

            _fileRepository.Remove(file);

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
