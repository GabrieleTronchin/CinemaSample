

using Cinema.Domain;
using System.Text.Json;

namespace Cinema.Persistence.Configuration.AuditoriumDefinition;

internal class AuditoriumConfiguration : IEntityTypeConfiguration<AuditoriumEntity>
{
    public void Configure(EntityTypeBuilder<AuditoriumEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(s => s.Seats)
            .WithOne()
            .HasForeignKey(s => s.AuditoriumId);
    }
}
