using Cinema.Application.Queries.Showtime.Models;

namespace Cinema.Application.Queries.Auditorium.Models
{
    public class AuditoriumReadModel
    {
        public int Id { get; set; }
        public List<ShowTimeReadModel> Showtimes { get; set; }
        public ICollection<SeatReadModel> Seats { get; set; }

    }
}
