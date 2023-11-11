

using Cinema.Domain;
using Cinema.Domain.Ticket;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.HasKey(t => t.Id);

        //TODO Check if works with real Db
        builder.Property(t => t.Seats)
               .HasConversion(
                  seats => JsonSerializer.Serialize(seats, new JsonSerializerOptions()),
                  value => JsonSerializer.Deserialize<List<Seat>>(value, new JsonSerializerOptions())
                );
    }
}

