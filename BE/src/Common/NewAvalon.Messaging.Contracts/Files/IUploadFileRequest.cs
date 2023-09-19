namespace NewAvalon.Messaging.Contracts.Files
{
    public interface IUploadFileRequest
    {
        string Name { get; set; }

        byte[] Bytes { get; set; }
    }
}
