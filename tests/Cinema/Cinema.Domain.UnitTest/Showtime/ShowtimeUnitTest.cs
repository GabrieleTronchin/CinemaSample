using Cinema.Domain.AuditoriumDefinition;
using Cinema.Domain.Showtime;

namespace Cinema.Domain.UnitTest.Showtime;

public class ShowtimeUnitTest
{
    [Fact]
    public void Create()
    {

        var auditorium = AuditoriumEntity.Create(0, Utility.GenerateSeats(1, 1));

        var movie = MovieEntity.Create("Test","Test","Test",DateTime.Now);
        var entity = ShowtimeEntity.Create(auditorium, movie, DateTime.UtcNow);

        Assert.NotNull(entity);
    }

    [Fact]
    public void Create_NotAllowed()
    {
        var auditorium = AuditoriumEntity.Create(0, Utility.GenerateSeats(1, 1));
        var movie = MovieEntity.Create("Test", "Test", "Test", DateTime.Now);
        var ex1 = Assert.Throws<ArgumentNullException>(() => ShowtimeEntity.Create(null, movie, DateTime.UtcNow));

        Assert.Equal("Value cannot be null. (Parameter 'auditorium')", ex1.Message);

        var ex2 = Assert.Throws<ArgumentNullException>(() => ShowtimeEntity.Create(auditorium, null, DateTime.UtcNow));

        Assert.Equal("Value cannot be null. (Parameter 'movie')", ex2.Message);
    }

}