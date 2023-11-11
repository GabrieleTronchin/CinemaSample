using Cinema.Application.Queries.Auditorium.Models;

namespace Cinema.Application.Queries
{
    public class ShowTimeReadModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Stars { get; set; }
        public DateTime SessionDate { get; set; }
        public IEnumerable<SeatReadModel> Seats { get; set; }
    }
}
