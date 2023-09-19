using MassTransit;
using NewAvalon.Messaging.Contracts.Files;
using NewAvalon.Storage.Business.Contracts.Files;
using NewAvalon.Storage.Domain.Entities;
using NewAvalon.Storage.Domain.EntityIdentifiers;
using NewAvalon.Storage.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Business.Files.Consumers
{
    public sealed class FileListRequestConsumer : IConsumer<IFileListRequest>
    {
        private readonly IFileRepository _fileRepository;

        public FileListRequestConsumer(IFileRepository fileRepository) => _fileRepository = fileRepository;

        public async Task Consume(ConsumeContext<IFileListRequest> context)
        {
            File[] files = await _fileRepository.GetByIdsAsync(
                context.Message.FileIds.Select(fileId => new FileId(fileId)).ToArray(),
                context.CancellationToken);

            var response = new FileListResponse
            {
                Files = files.Select(file => new FileResponse
                {
                    Id = file.Id.Value,
                    Url = file.Url,
                    Name = file.Name,
                    Extension = file.Extension,
                    Size = file.Size
                }).Cast<IFileResponse>().ToArray()
            };

            await context.RespondAsync<IFileListResponse>(response);
        }
    }
}
