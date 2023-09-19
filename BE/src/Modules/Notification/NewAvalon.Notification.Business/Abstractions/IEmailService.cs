using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Business.Abstractions
{
    public interface IEmailService
    {
        Task SendEmail(string senderEmail, string recieverEmail, string content, CancellationToken cancellationToken);
    }
}
