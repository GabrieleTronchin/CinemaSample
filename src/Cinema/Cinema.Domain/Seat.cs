namespace Cinema.Domain;


// ROW number is better to be a letter, i leave row short just to back compatibility
public record Seat
{
    public short SeatNumber { get; private set; }
    public short RowNumber { get; private set; }
    public string Code { get; private set; } = string.Empty;

    public static Seat Create(string seatCode) {

        if (string.IsNullOrWhiteSpace(seatCode)) throw new ArgumentNullException(nameof(seatCode));
        var codes = seatCode.Split(',');

        if (short.TryParse(codes[0], out short rowNumber))
            throw new ArgumentNullException(nameof(seatCode));

        if (short.TryParse(codes[0], out short seatNumber))
            throw new ArgumentNullException(nameof(seatCode));

        return new Seat()
        {
            Code = seatCode,
            SeatNumber = seatNumber,
            RowNumber = rowNumber
        };

    }

    public static Seat Create(short rowNumber, short seatNumber)
    {
        return new Seat()
        {
            Code = $"{rowNumber},{seatNumber}",
            SeatNumber = seatNumber,
            RowNumber = rowNumber
        };
    }



};
