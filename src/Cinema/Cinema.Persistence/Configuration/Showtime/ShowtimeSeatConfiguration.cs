using Cinema.Domain;
using Cinema.Domain.Showtime;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.Showtime;

internal class ShowtimeSeatConfiguration : IEntityTypeConfiguration<ShowtimeSeatEntity>
{

    public void Configure(EntityTypeBuilder<ShowtimeSeatEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Seat)
            .HasConversion(
                    seat => seat.Code,
                    value => Seat.Create(value)
                );

    }
}

