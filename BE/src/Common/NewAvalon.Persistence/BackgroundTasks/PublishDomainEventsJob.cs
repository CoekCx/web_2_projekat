using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Persistence.Options;
using NewAvalon.Persistence.Relational.Outbox;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Persistence.BackgroundTasks
{
    [DisallowConcurrentExecution]
    public abstract class PublishDomainEventsJob : IJob
    {
        private readonly DbContext _dbContext;
        private readonly IPublisher _publisher;
        private readonly IPublishDomainEventsJobOptions _options;
        private readonly ILogger<PublishDomainEventsJob> _logger;
        private readonly ISystemTime _systemTime;

        protected PublishDomainEventsJob(
            DbContext dbContext,
            IPublisher publisher,
            IPublishDomainEventsJobOptions options,
            ISystemTime systemTime,
            ILogger<PublishDomainEventsJob> logger)
        {
            _dbContext = dbContext;
            _publisher = publisher;
            _options = options;
            _systemTime = systemTime;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                string module = _dbContext.GetType().Name.Replace("DbContext", string.Empty);

                _logger.LogInformation("Starting job for module: {@Module}, {@UtcNow}", module, _systemTime.UtcNow);

                List<Message> messages = await GetUnprocessedMessages(context);

                _logger.LogInformation("Module: {@Module}, {@MessageCount} unprocessed messages in batch", module, messages.Count);

                if (!messages.Any())
                {
                    return;
                }

                foreach (Message message in messages)
                {
                    try
                    {
                        _logger.LogInformation(
                            "Started processing message {@MessageId}, module: {@Module}, {@UtcNow}",
                            message.Id,
                            module,
                            _systemTime.UtcNow);

                        IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                        _logger.LogInformation(
                            "Publishing message {@MessageId}, module: {@Module}, {@UtcNow}",
                            message.Id,
                            module,
                            _systemTime.UtcNow);

                        await _publisher.Publish(domainEvent, context.CancellationToken);

                        _logger.LogInformation(
                            "Marking message as completed {@MessageId}, module: {@Module}, {@UtcNow}",
                            message.Id,
                            module,
                            _systemTime.UtcNow);

                        message.MarkAsProcessed();

                        await _dbContext.SaveChangesAsync(context.CancellationToken);

                        _logger.LogInformation(
                            "Finished processing message {@MessageId}, module: {@Module}, {@UtcNow}",
                            message.Id,
                            module,
                            _systemTime.UtcNow);
                    }
                    catch (Exception exception)
                    {
                        message.Retry(_options.RetryCountThreshold, exception.ToString());

                        _logger.LogError(
                            exception,
                            "Exception while processing message: {@MessageId}, retry count: {@RetryCount}",
                            message.Id,
                            message.Retries);

                        await _dbContext.SaveChangesAsync(context.CancellationToken);
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Unexpected exception occurred with messages: {@ExceptionMessage}",
                    exception.Message);
            }
        }

        private async Task<List<Message>> GetUnprocessedMessages(IJobExecutionContext context) =>
            await _dbContext.Set<Message>()
                .OrderBy(message => message.CreatedOnUtc)
                .Where(message => !message.Processed && !message.Failed)
                .Take(20)
                .ToListAsync(context.CancellationToken);
    }
}
