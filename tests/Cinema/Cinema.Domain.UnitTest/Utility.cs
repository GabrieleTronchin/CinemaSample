namespace Cinema.Domain.UnitTest;

public static class Utility
{
    public static List<Seat> GenerateSeats(short rows, short seatsPerRow)
    {
        var seats = new List<Seat>();
        for (short r = 1; r <= rows; r++)
            for (short s = 1; s <= seatsPerRow; s++)
                seats.Add(new Seat(r, s));
        return seats;
    }
}
