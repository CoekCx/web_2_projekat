using System;

namespace NewAvalon.Messaging.Contracts.Files
{
    public interface IStoreUploadedFileRequest
    {
        public Guid FileId { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public decimal Size { get; set; }
    }
}
