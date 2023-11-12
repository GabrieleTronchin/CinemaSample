using Cinema.Domain.Ticket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Persistence.Repositories
{
    public sealed class TicketDomainEventInterceptor
    : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var events = dbContext.ChangeTracker.Entries<TicketEntity>()
                 .Select(x => x.Entity)
                 .SelectMany(x =>
                 {
                     var ticketDE = x.DomainEventsList;

                     x.DomainEventsList.Clear();

                     return ticketDE;

                 })
                 .ToList();


            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
