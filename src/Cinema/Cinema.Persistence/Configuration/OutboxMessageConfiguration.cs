using Cinema.Domain;

namespace Cinema.Persistence.Configuration;

internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessageEntity>
{
    public void Configure(EntityTypeBuilder<OutboxMessageEntity> builder)
    {
        builder.HasKey(t => t.Id);
    }
}

