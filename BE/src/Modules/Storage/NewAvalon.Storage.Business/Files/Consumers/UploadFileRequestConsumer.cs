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
    public sealed class UploadFileRequestConsumer : IConsumer<IUploadFileRequest>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileUrlFormatter _fileUrlFormatter;
        private readonly IFileRepository _fileRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public UploadFileRequestConsumer(
            IFileStorageService fileStorageService,
            IFileUrlFormatter fileUrlFormatter,
            IFileRepository fileRepository,
            IStorageUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _fileUrlFormatter = fileUrlFormatter;
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<IUploadFileRequest> context)
        {
            IUploadFileRequest uploadFileRequest = context.Message;

            Guid fileId = await _fileStorageService.UploadAsync(uploadFileRequest.Bytes, context.CancellationToken);

            Uri url = _fileUrlFormatter.Format(fileId);

            int lastIndexOfDot = uploadFileRequest.Name.LastIndexOf('.');

            string name = uploadFileRequest.Name.Substring(0, lastIndexOfDot);

            string extension = uploadFileRequest.Name.Substring(lastIndexOfDot);

            var file = new File(new FileId(fileId), url.ToString(), name, extension, uploadFileRequest.Bytes.Length);

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
