using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Persistence.Relational.Outbox
{
    public sealed class Message : IAuditableEntity
    {
        public Message(Guid id, string content)
            : base()
        {
            Id = id;
            Content = content;
        }

        private Message()
        {
        }

        public Guid Id { get; private set; }

        public string Content { get; private set; }

        public bool Processed { get; private set; }

        public int Retries { get; private set; }

        public bool Failed { get; private set; }

        public string Error { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public void MarkAsProcessed() => Processed = true;

        public void Retry(int retryThreshold, string error)
        {
            Error = error;
            Retries++;

            if (Retries < retryThreshold)
            {
                return;
            }

            Failed = true;
        }
    }
}
