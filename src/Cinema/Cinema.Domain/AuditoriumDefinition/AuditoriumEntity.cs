namespace Cinema.Domain.AuditoriumDefinition;
public class AuditoriumEntity
{
    public static AuditoriumEntity Create(int Id, ICollection<Seat> seats)
    {
        return new AuditoriumEntity
        {
            Id = Id,
            Seats = seats
        };
    }

    public int Id { get; private set; }
    public ICollection<Seat> Seats { get; private set; }
}