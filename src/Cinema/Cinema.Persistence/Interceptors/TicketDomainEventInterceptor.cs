using Cinema.Domain;
using Cinema.Domain.Ticket;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Cinema.Persistence.Interceptors
{
    public sealed class TicketDomainEventInterceptor
    : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);


            // this is interceptor has been created to automatically save domain event when an entity is saved.
            // now it works only with ticket but could be generalized for all entity
            var domainEvents = dbContext.ChangeTracker.Entries<TicketEntity>()
                 .Select(x => x.Entity)
                 .SelectMany(x =>
                 {
                     var ticketDE = x.Events;

                     x.Events.Clear();

                     return ticketDE;

                 })
                 .Select(x => new DomainEvent()
                 {
                     Id = Guid.NewGuid(),
                     CreationTime = DateTime.UtcNow,
                     Type = x.GetType().Name,
                     Content = JsonConvert.SerializeObject(x, new JsonSerializerSettings
                     {
                         TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
                     })
                 })
                 .ToList();

            dbContext.Set<DomainEvent>().AddRange(domainEvents);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
