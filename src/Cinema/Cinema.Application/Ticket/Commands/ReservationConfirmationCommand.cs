using MediatR;

namespace Cinema.Application.Ticket.Commands;

public class ReservationConfirmationCommand : IRequest<ReservationConfirmationComplete>
{
    public Guid ReservationId { get; set; }
}

public record ReservationConfirmationComplete
{
    public bool Success { get; set; }
}
