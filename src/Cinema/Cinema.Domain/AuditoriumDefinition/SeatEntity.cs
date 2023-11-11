namespace Cinema.Domain.AuditoriumDefinition;

//TODO Could be switch to value type?! ( record )
public class SeatEntity
{
    public Guid Id { get; private set; }
    public int AuditoriumId { get; private set; }
    public short SeatNumber { get; private set; }
    public short RowNumber { get; private set; }
    public static SeatEntity Create(int auditoriumId, short seatNumber, short rowNumber) {

        return new SeatEntity()
        {
            Id = Guid.NewGuid(),
            AuditoriumId = auditoriumId,
            SeatNumber = seatNumber,
            RowNumber = rowNumber
        };

    }
}
