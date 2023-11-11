

using Cinema.Domain;
using Cinema.Domain.AuditoriumDefinition;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class AuditoriumConfiguration : IEntityTypeConfiguration<AuditoriumEntity>
{
    public void Configure(EntityTypeBuilder<AuditoriumEntity> builder)
    {
        builder.HasKey(entry => entry.Id);
        builder.Property(entry => entry.Id).ValueGeneratedOnAdd();
        builder.OwnsMany(a => a.Seats, seatBuilder =>
            {
                seatBuilder.Property(s => s.RowNumber);
                seatBuilder.Property(s => s.SeatNumber);
            });
    }
}
