using MassTransit;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Business.Contracts.Files;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Consumers
{
    public class StoreUploadedFileRequestConsumer : IConsumer<IStoreUploadedFileRequest>
    {
        private readonly IFileUrlFormatter _fileUrlFormatter;
        private readonly IFileRepository _fileRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public StoreUploadedFileRequestConsumer(
            IFileUrlFormatter fileUrlFormatter,
            IFileRepository fileRepository,
            IStorageUnitOfWork unitOfWork)
        {
            _fileUrlFormatter = fileUrlFormatter;
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IStoreUploadedFileRequest> context)
        {
            IStoreUploadedFileRequest storeUploadedFileRequest = context.Message;

            Uri url = _fileUrlFormatter.Format(storeUploadedFileRequest.FileId);

            var file = new File(
                new FileId(storeUploadedFileRequest.FileId),
                url.ToString(),
                storeUploadedFileRequest.Name,
                storeUploadedFileRequest.Extension,
                storeUploadedFileRequest.Size);

            _fileRepository.Insert(file);

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);

            var fileResponse = new FileResponse
            {
                Id = file.Id.Value,
                Url = file.Url,
                Name = file.Name,
                Extension = file.Extension,
                Size = file.Size
            };

            await context.RespondAsync<IFileResponse>(fileResponse);
        }
    }
}
