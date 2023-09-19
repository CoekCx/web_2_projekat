using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Notification.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Infrastructure.Services
{
    internal sealed class EmailService : IEmailService, ITransient
    {
        public EmailService()
        {
            throw new System.NotImplementedException();
        }

        public Task SendEmail(string senderEmail, string recieverEmail, string content, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
