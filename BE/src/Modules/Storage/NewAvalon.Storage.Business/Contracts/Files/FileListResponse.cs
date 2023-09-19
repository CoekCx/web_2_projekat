using NewAvalon.Messaging.Contracts.Files;

namespace NewAvalon.Storage.Business.Contracts.Files
{
    internal sealed class FileListResponse : IFileListResponse
    {
        public IFileResponse[] Files { get; set; }
    }
}
