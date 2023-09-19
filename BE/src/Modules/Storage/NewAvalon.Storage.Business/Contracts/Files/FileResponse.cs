using NewAvalon.Messaging.Contracts.Files;
using System;

namespace NewAvalon.Storage.Business.Contracts.Files
{
    internal sealed class FileResponse : IFileResponse
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public decimal Size { get; set; }
    }
}
