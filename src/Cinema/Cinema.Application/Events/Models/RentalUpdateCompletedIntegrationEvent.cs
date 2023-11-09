namespace Cinema.Application.Events.Models;

public class RentalUpdateCompletedIntegrationEvent : IntegrationEvent
{
    public int Id { get; set; }
    public int Units { get; set; }
}

