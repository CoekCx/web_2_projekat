using System;

namespace NewAvalon.Storage.Business.Abstractions
{
    public interface IImageUrlFormatter
    {
        Uri Format(Guid imageId);
    }
}
