using Cinema.Domain.Showtime;

namespace Cinema.Domain.AuditoriumDefinition;
public class AuditoriumEntity
{
    public static AuditoriumEntity Create(int Id, IEnumerable<Seat> seats)
    {
        return new AuditoriumEntity
        {
            Id = Id,
            Seats = seats
        };
    }

    public int Id { get; private set; }
    public IEnumerable<Seat> Seats { get; private set; } = Enumerable.Empty<Seat>();
}
