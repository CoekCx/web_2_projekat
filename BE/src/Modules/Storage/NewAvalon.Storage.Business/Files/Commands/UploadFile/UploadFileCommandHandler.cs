using Microsoft.AspNetCore.Http;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Storage.Boundary.Files.Commands.UploadFile;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Commands.UploadFile
{
    internal sealed class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, UploadFileResponse>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileUrlFormatter _fileUrlFormatter;
        private readonly IFileRepository _fileRepository;
        private readonly IStorageUnitOfWork _unitOfWork;

        public UploadFileCommandHandler(
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

        public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            IFormFile formFile = request.File;

            Guid fileId = await _fileStorageService.UploadAsync(formFile, cancellationToken);

            Uri url = _fileUrlFormatter.Format(fileId);

            int lastIndexOfDot = formFile.FileName.LastIndexOf('.');

            string name = formFile.FileName.Substring(0, lastIndexOfDot);

            string extension = formFile.FileName.Substring(lastIndexOfDot);

            var file = new File(new FileId(fileId), url.ToString(), name, extension, formFile.Length);

            _fileRepository.Insert(file);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UploadFileResponse(file.Id.Value, file.Url, file.Name, file.Extension, file.Size);
        }
    }
}
