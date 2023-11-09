using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.Commands
{
    public class ConfirmReservationCommand : IRequest<ConfirmReservationCommandCompleted>
    {
        public string Id { get; set; }
    }

    public record ConfirmReservationCommandCompleted
    {
        public string Id { get; set; }
    }
}
