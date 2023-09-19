namespace NewAvalon.Messaging.Contracts.Products
{
    public interface IIsProductUsedResponse
    {
        public bool IsUsed { get; set; }

        public bool IsCurrentlyInUse { get; set; }
    }
}
