using NewAvalon.Domain.Errors;
using System;

namespace NewAvalon.Domain.Exceptions.Files
{
    public sealed class FileAlreadyAttachedException : ConflictException
    {
        public FileAlreadyAttachedException(Guid fileId)
            : base("File already attached", $"The file with the identifier {fileId} was already attached.", Error.FileAlreadyAttached)
        {
        }
    }
}
