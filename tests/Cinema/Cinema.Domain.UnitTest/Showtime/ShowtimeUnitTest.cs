using Cinema.Domain.AuditoriumDefinition;
using Cinema.Domain.Showtime;

namespace Cinema.Domain.UnitTest.Ticket;

public class ShowtimeUnitTest
{
    [Fact]
    public void Create()
    {

        var auditorium = AuditoriumEntity.Create(0, Utility.GenerateSeats(1, 1));

        var test = new MovieEntity();
        var entity = ShowtimeEntity.Create(auditorium, test, DateTime.UtcNow);

        Assert.NotNull(entity);
    }

    [Fact]
    public void Create_NotAllowed()
    {

        var auditorium = AuditoriumEntity.Create(0, Utility.GenerateSeats(1, 1));

        var test = new MovieEntity();
        Assert.Throws<ArgumentException>(() => ShowtimeEntity.Create(auditorium, test, DateTime.UtcNow));
    }

}