using Cinema.Domain.AuditoriumDefinition;

namespace Cinema.Domain.Showtime;
public class ShowtimeSeatEntity
{
    const short DEFAULT_COOLDOWN = 10;

    public static ShowtimeSeatEntity Create(SeatEntity seat, Guid showtimeId)
    {
        return new ShowtimeSeatEntity
        {
            Id = Guid.NewGuid(),
            ShowtimeId = showtimeId,
            ReservationCooldown = TimeSpan.FromMinutes(DEFAULT_COOLDOWN),
            Purchased = false,
            ReservationTime = null,
            AuditoriumId = seat.AuditoriumId,
            SeatNumber = seat.SeatNumber,
            RowNumber = seat.RowNumber,
        };
    }


    public void SetReserved()
    {
        if (Purchased)
            throw new InvalidOperationException("It shouldn't be possible to reserve an already sold seat.");

        if (DateTime.UtcNow >= DateTime.UtcNow.Add(ReservationCooldown))
            throw new InvalidOperationException($"It should not be possible to reserve the same seats two times in {DEFAULT_COOLDOWN} minutes");


        ReservationTime = DateTime.UtcNow;
    }

    public void SetPurchased()
    {
        if (Purchased) throw new InvalidOperationException(" It is not possible to buy two times the same seat.");
        Purchased = true;
    }

    public Guid Id { get; private set; }
    public Guid ShowtimeId { get; private set; }
    public int AuditoriumId { get; private set; }
    public short SeatNumber { get; private set; }
    public short RowNumber { get; private set; }
    public TimeSpan ReservationCooldown { get; private set; }
    public DateTime? ReservationTime { get; private set; }
    public bool Purchased { get; private set; }

}
