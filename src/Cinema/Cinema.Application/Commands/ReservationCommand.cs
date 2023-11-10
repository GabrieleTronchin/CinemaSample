using MediatR;

namespace Cinema.Application.Commands
{
    public class ReservationCommand : IRequest<ReservationComplete>
    {
        public int ShowtimeId { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public int AuditoriumId { get; set; }
    }

    public record Seat
    {
        public short SeatsNumber { get; set; }
        public short Row { get; set; }
    }

    public record ReservationComplete
    {
        public Guid Id { get; set; }

        public IEnumerable<Seat> SeatsNumber { get; set; }

        public int AuditoriumId { get; set; }

        public string MovieTitle { get; set; }
    }
}
