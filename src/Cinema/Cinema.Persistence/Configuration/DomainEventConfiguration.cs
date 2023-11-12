using Cinema.Domain;

namespace Cinema.Persistence.Configuration;

internal class DomainEventConfiguration : IEntityTypeConfiguration<DomainEvent>
{
    public void Configure(EntityTypeBuilder<DomainEvent> builder)
    {
        builder.HasKey(t => t.Id);
    }
}

