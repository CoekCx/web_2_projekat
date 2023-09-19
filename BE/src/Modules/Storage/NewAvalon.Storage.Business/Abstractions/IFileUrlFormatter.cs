using System;

namespace NewAvalon.Storage.Business.Abstractions
{
    public interface IFileUrlFormatter
    {
        Uri Format(Guid fileId);
    }
}
