

using Cinema.Domain.Showtime;

namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class MovieEntityConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.HasKey(t => t.Id);

        //Add entity db constraints


        builder.HasIndex(t => t.Title); //example
    }
}

