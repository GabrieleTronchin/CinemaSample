using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Application.Commands
{
    public class AssignShowtimeCommand : IRequest<AssignShowtimeCommandComplete>
    {

        public string ImdbId { get; set; }

        public DateTime SessionDate { get; set; }

        public int AuditoriumId { get; set; }
    }

    public record AssignShowtimeCommandComplete
    {
        public string Id { get; set; }
    }
}
