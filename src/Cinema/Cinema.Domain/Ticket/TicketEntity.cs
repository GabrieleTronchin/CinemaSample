using Cinema.Domain.AuditoriumDefinition;

namespace Cinema.Domain.Ticket
{
    public class TicketEntity
    {

        //Use Seat record or SeatEntity?
        public static TicketEntity Create(IEnumerable<SeatEntity> seats, Guid showtimeId, string movieTile)
        {
            if (!seats.Any()) throw new ArgumentException($"Invalid {seats}");

            if (string.IsNullOrWhiteSpace(movieTile)) throw new ArgumentException($"Invalid {movieTile}");

            var ticket = new TicketEntity
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                Paid = false,
                Seats = string.Join(",", seats.Select(s => $"{s.RowNumber}{s.SeatNumber}")),
                MovieTitle = movieTile,
                ShowtimeId = showtimeId,
            };
            return ticket;
        }

        public void ConfirmPaymentAsync()
        {
            if (this.Paid) throw new InvalidOperationException("It's already paid.");

            this.Paid = true;
        }

        public Guid Id { get; private set; }
        public Guid ShowtimeId { get; private set; }
        public string MovieTitle { get; private set; } = string.Empty;
        public string Seats { get; private set; } = string.Empty;
        public DateTime CreatedTime { get; private set; }
        public bool Paid { get; private set; }
    }

}
