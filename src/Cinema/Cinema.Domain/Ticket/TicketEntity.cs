namespace Cinema.Domain.Ticket
{
    public class TicketEntity
    {
        public static TicketEntity Create(IEnumerable<Seat> seats, int showtimeId, string movieTile)
        {

            if (!seats.Any()) throw new ArgumentException($"Invalid {seats}");

            if (string.IsNullOrWhiteSpace(movieTile)) throw new ArgumentException($"Invalid {movieTile}");

            var ticket = new TicketEntity
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.Now,
                Paid = false,
                Seats = seats,
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
        public int ShowtimeId { get; private set; }
        public string MovieTitle { get; private set; }
        public IEnumerable<Seat> Seats { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public bool Paid { get; private set; }
    }

}
