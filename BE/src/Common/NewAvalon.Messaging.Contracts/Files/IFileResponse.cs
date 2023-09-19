using System;

namespace NewAvalon.Messaging.Contracts.Files
{
    public interface IFileResponse
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public decimal Size { get; set; }
    }
}
