using Cinema.Domain;
using Cinema.Domain.Showtime;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.Showtime;

internal class ShowtimeSeatConfiguration : IEntityTypeConfiguration<ShowtimeSeatEntity>
{

    public void Configure(EntityTypeBuilder<ShowtimeSeatEntity> builder)
    {
        builder.HasKey(t => t.Id);

        // Configure Seat as Owned Entity to store it as a complex type
        builder.OwnsOne(a => a.Seat, seatBuilder =>
        {
            seatBuilder.Property(s => s.RowNumber);
            seatBuilder.Property(s => s.SeatNumber);
        });
    }
}

