using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Exceptions.Files;
using NewAvalon.Storage.Boundary.Files.Queries.DownloadFile;
using NewAvalon.Storage.Business.Abstractions;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Queries.DownloadFile
{
    internal sealed class DownloadFileQueryHandler : IQueryHandler<DownloadFileQuery, DownloadFileResponse>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileRepository _fileRepository;

        public DownloadFileQueryHandler(IFileStorageService fileStorageService, IFileRepository fileRepository)
        {
            _fileStorageService = fileStorageService;
            _fileRepository = fileRepository;
        }

        public async Task<DownloadFileResponse> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            File file = await _fileRepository.GetByIdAsync(new FileId(request.FileId), cancellationToken);

            if (file is null)
            {
                throw new FileNotFoundException(request.FileId);
            }

            byte[] bytes = await _fileStorageService.DownloadAsync(request.FileId, cancellationToken);
            string fileName = $"{file.Name}{file.Extension}";
            string contentType = MimeTypes.GetMimeType(fileName);

            return new DownloadFileResponse(
                file.Id.Value,
                fileName,
                contentType,
                bytes);
        }
    }
}
