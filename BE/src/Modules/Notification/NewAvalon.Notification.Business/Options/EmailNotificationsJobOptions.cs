namespace NewAvalon.Notification.Business.Options
{
    public class EmailNotificationsJobOptions
    {
        public int BatchSize { get; set; }

        public string ApiKey { get; set; }

        public int IntervalInSeconds { get; set; }

        public string Sender { get; set; }
    }
}
