

using Cinema.Domain.Showtime;

namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class ShowtimeConfiguration : IEntityTypeConfiguration<ShowtimeEntity>
{
    public void Configure(EntityTypeBuilder<ShowtimeEntity> builder)
    {
        builder.HasKey(t => t.Id);


        // FK to ShowtimeSeat 1-N


        //FK to Movie 1-1
    }
}

