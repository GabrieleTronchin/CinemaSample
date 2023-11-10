using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.Commands
{
    public class ReservationConfirmationCommand : IRequest<ReservationConfirmationComplete>
    {
        public Guid ReservationId { get; set; }
    }


    public record ReservationConfirmationComplete
    {
        public bool Success { get; set; }
    }
}
