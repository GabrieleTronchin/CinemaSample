using Cinema.Domain.Primitives;
using Cinema.Domain.Ticket.Events;

namespace Cinema.Domain.Ticket
{
    public class TicketEntity
    {
        //Use Seat record or SeatEntity?
        public static TicketEntity Create(IEnumerable<Seat> seats, Guid showtimeId, string movieTile)
        {
            if (!seats.Any()) throw new ArgumentException($"Invalid {seats}");

            if (string.IsNullOrWhiteSpace(movieTile)) throw new ArgumentException($"Invalid {movieTile}");

            var ticket = new TicketEntity
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                Paid = false,
                Seats = seats.ToList(),
                MovieTitle = movieTile,
                ShowtimeId = showtimeId,
            };

            return ticket;
        }

        public IList<IDomainEvent> Events { get; private set; } = new List<IDomainEvent>();

        public void ConfirmPayment()
        {
            if (Paid) throw new InvalidOperationException("It's already paid.");

            Events.Add(new PaymentAccepted(ShowtimeId, Seats));

            Paid = true;
        }

        public Guid Id { get; private set; }
        public Guid ShowtimeId { get; private set; }
        public string MovieTitle { get; private set; } = string.Empty;
        public ICollection<Seat> Seats { get; private set; } = new List<Seat>();
        public DateTime CreatedTime { get; private set; }
        public bool Paid { get; private set; }
    }

}
