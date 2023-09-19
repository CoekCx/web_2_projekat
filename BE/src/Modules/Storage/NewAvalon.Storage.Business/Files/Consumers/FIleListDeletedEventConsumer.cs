using MassTransit;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Consumers
{
    public class FIleListDeletedEventConsumer : IConsumer<IFileListDeletedEvent>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileRepository _fileRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public FIleListDeletedEventConsumer(
            IFileStorageService fileStorageService,
            IFileRepository fileRepository,
            IStorageUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IFileListDeletedEvent> context)
        {
            foreach (Guid fileId in context.Message.FileIds)
            {
                await _fileStorageService.DeleteAsync(fileId, context.CancellationToken);

                File file = await _fileRepository.GetByIdAsync(new FileId(fileId), context.CancellationToken);

                if (file is null)
                {
                    return;
                }

                _fileRepository.Remove(file);
            }

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }
}
