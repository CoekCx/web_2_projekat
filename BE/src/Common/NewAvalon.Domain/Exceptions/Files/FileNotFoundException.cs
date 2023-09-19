using NewAvalon.Domain.Errors;
using System;

namespace NewAvalon.Domain.Exceptions.Files
{
    public sealed class FileNotFoundException : NotFoundException
    {
        public FileNotFoundException(Guid fileId)
            : base("File not found", $"The file with the identifier {fileId} was not found.", Error.FileNotFound)
        {
        }
    }
}
