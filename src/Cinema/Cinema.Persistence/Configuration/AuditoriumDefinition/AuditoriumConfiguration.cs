

using Cinema.Domain;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class AuditoriumConfiguration : IEntityTypeConfiguration<AuditoriumEntity>
{
    public void Configure(EntityTypeBuilder<AuditoriumEntity> builder)
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

