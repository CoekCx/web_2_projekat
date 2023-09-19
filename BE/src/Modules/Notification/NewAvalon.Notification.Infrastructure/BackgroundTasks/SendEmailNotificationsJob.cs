using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewAvalon.Domain.Enums;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Notification.Business.Options;
using NewAvalon.Notification.Persistence;
using Quartz;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Infrastructure.BackgroundTasks
{
    [DisallowConcurrentExecution]
    public sealed class SendEmailNotificationsJob : IJob
    {
        private readonly NotificationDbContext _dbContext;
        private readonly IRequestClient<IUserDetailsListRequest> _userDetailsListRequestClient;
        private readonly ILogger<SendEmailNotificationsJob> _logger;
        private readonly EmailNotificationsJobOptions _options;
        private readonly EmailTemplatesOptions _emailTemplatesOptions;
        private readonly ISendGridClient _sendGridClient;

        public SendEmailNotificationsJob(
            NotificationDbContext dbContext,
            IRequestClient<IUserDetailsListRequest> userDetailsListRequestClient,
            ILogger<SendEmailNotificationsJob> logger,
            IOptions<EmailNotificationsJobOptions> options,
            IOptions<EmailTemplatesOptions> emailTemplatesOptions,
            ISendGridClient sendGridClient)
        {
            _dbContext = dbContext;
            _userDetailsListRequestClient = userDetailsListRequestClient;
            _logger = logger;
            _options = options.Value;
            _emailTemplatesOptions = emailTemplatesOptions.Value;
            _sendGridClient = sendGridClient;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting {@JobName} job.", nameof(SendEmailNotificationsJob));

            List<Domain.Entities.Notification> notifications = await _dbContext.Set<Domain.Entities.Notification>()
                .Where(notification =>
                    !notification.Published &&
                    !notification.Failed &&
                    notification.DeliveryMechanism == DeliveryMechanism.Email)
                .OrderBy(notification => notification.CreatedOnUtc)
                .Take(_options.BatchSize)
                .ToListAsync(context.CancellationToken);

            if (!notifications.Any())
            {
                return;
            }

            var userDetailsListRequest = new UserDetailsListRequest
            {
                UserIds = notifications
                    .Where(n => n.UserId.HasValue)
                    .Select(n => n.UserId.Value)
                    .Distinct()
                    .ToArray()
            };

            IUserDetailsListResponse userDetailsListResponse =
                (await _userDetailsListRequestClient.GetResponse<IUserDetailsListResponse>(
                    userDetailsListRequest,
                    context.CancellationToken))
                .Message;

            foreach (Domain.Entities.Notification notification in notifications)
            {
                try
                {
                    await _sendGridClient.SendEmailAsync(CreateSendGridMessage(userDetailsListResponse.Users.First(), notification.Type));

                    notification.Publish(DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed sending an email notification '{notification.Id.Value}' for user '{notification.UserId}'", ex);

                    notification.Fail(DateTime.UtcNow);
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        private SendGridMessage CreateSendGridMessage(IUserDetailsResponse userDetails, NotificationType type)
        {
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(_options.Sender, "NewAvalon"),
                TemplateId = GetTemplateName(type),
            };

            sendGridMessage.AddTo(new EmailAddress(userDetails.Email, userDetails.Username));

            return sendGridMessage;
        }

        private string GetTemplateName(NotificationType type) =>
            type switch
            {
                NotificationType.DealerUserApproved => _emailTemplatesOptions.UserVerifiedTemplate,
                _ => _emailTemplatesOptions.UserVerifiedTemplate,
            };

        private sealed class UserDetailsListRequest : IUserDetailsListRequest
        {
            public Guid[] UserIds { get; set; }
        }
    }
}