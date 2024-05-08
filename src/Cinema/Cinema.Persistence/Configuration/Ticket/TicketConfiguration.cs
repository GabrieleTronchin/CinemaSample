using Cinema.Domain.Ticket;

namespace Cinema.Persistence.Configuration.Ticket;

internal class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.HasKey(t => t.Id);
        builder.OwnsMany(
            a => a.Seats,
            seatBuilder =>
            {
                seatBuilder.Property(s => s.RowNumber);
                seatBuilder.Property(s => s.SeatNumber);
            }
        );
    }
}
