using Cinema.Domain.Showtime;

namespace Cinema.Persistence.Configuration.Showtime;

internal class MovieEntityConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Title); //example
    }
}

