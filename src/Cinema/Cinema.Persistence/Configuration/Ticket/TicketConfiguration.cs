using Cinema.Domain;
using Cinema.Domain.Ticket;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.Ticket;

internal class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.HasKey(t => t.Id);

    }
}

