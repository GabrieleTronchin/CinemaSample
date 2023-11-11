namespace Cinema.Domain.Showtime;
public class ShowtimeSeatEntity
{
    const short DEFAULT_COOLDOWN = 10;

    public static ShowtimeSeatEntity Create(Seat seat)
    {
        return new ShowtimeSeatEntity {
            Id = Guid.NewGuid(),
            ReservationCooldown = TimeSpan.FromMinutes(DEFAULT_COOLDOWN),
            Purchased = false,
            ReservationTime = null,
            Seat = seat
        };
    }


    public void SetReserved() {
        if (Purchased) throw new InvalidOperationException("Already sold.");
        
        if (DateTime.UtcNow >= DateTime.UtcNow.Add(ReservationCooldown))
            throw new InvalidOperationException("Reserved.");

        ReservationTime = DateTime.UtcNow;
    }

    public void SetPurchased()
    {
        if (Purchased) throw new InvalidOperationException("Already sold.");
        Purchased = true;
    }

    public Guid Id { get; private set; }
    public Seat Seat { get; private set; }
    public TimeSpan ReservationCooldown { get; private set; }
    public DateTime? ReservationTime { get; private set; }
    public bool Purchased { get; private set; }

}
