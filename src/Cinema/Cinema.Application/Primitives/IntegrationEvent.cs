using System.Diagnostics.CodeAnalysis;

namespace Cinema.Application.Events;

[ExcludeFromCodeCoverage]
public abstract class IntegrationEvent
{
    public IntegrationEvent()
    {
        CorrelatorId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public Guid CorrelatorId { get; private init; }

    public DateTime CreationDate { get; private init; }

    public override string ToString()
    {
        return $"{nameof(CorrelatorId)}:{CorrelatorId} ; {nameof(CreationDate)}:{CreationDate}";
    }
}

