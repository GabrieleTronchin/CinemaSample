namespace Cinema.Application.Auditorium.Queries.Commands;

public class AuditoriumReadModel
{
    public int Id { get; set; }
    public IEnumerable<SeatReadModel> Seats { get; set; }
}
