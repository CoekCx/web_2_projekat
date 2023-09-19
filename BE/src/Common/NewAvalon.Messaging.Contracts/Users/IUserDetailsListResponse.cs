namespace NewAvalon.Messaging.Contracts.Users
{
    public interface IUserDetailsListResponse
    {
        public IUserDetailsResponse[] Users { get; set; }
    }
}