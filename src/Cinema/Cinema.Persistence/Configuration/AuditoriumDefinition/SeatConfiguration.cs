
namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class SeatConfiguration : IEntityTypeConfiguration<SeatEntity>
{

    public void Configure(EntityTypeBuilder<SeatEntity> builder)
    {
        builder.HasKey(entry => new { entry.AuditoriumId, entry.RowNumber, entry.SeatNumber });
        //builder.HasOne(entry => entry.Auditorium).WithMany(entry => entry.Seats).HasForeignKey(entry => entry.AuditoriumId);
    }
}
