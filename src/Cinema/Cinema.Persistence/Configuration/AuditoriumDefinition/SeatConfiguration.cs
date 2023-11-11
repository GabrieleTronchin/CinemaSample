
namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class SeatConfiguration : IEntityTypeConfiguration<SeatEntity>
{

    public void Configure(EntityTypeBuilder<SeatEntity> builder)
    {
        builder.HasKey(t => t.Id);
    }
}
