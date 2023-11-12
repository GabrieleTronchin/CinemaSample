using Cinema.Domain.Primitives;
using Cinema.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Cinema.Application;

[DisallowConcurrentExecution]
public class ProcessingDomainEventJob : IJob
{
    private const int DEFAULT_TAKE_DOMAINS = 10;
    private readonly CinemaDbContext _context;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessingDomainEventJob> _logger;

    public ProcessingDomainEventJob(ILogger<ProcessingDomainEventJob> logger,
                                         CinemaDbContext context,
                                          IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var domainEvents = await _context.DomainEvents
                 .Where(de => de.CompleteTime == null)
                 .Take(DEFAULT_TAKE_DOMAINS).ToListAsync(context.CancellationToken);


        if (!domainEvents.Any()) return;

        foreach (var dEvent in domainEvents)
        {
            var notification = JsonConvert.DeserializeObject<IDomainEvent>(dEvent.Content);

            if (notification == null)
            {
                _logger.LogError($"An error occurred during deserialization. Domain Event Id:{dEvent.Id}");
                continue;
            }

            await _publisher.Publish(notification, context.CancellationToken);

            dEvent.CompleteTime = DateTime.UtcNow;
        }


        await _context.SaveChangesAsync();
    }
}
