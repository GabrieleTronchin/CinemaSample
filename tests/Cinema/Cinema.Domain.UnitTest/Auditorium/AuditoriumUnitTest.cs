using Cinema.Domain.AuditoriumDefinition;

namespace Cinema.Domain.UnitTest.Auditorium;

public class AuditoriumUnitTest
{
    [Fact]
    public void Create()
    {

        var entity = AuditoriumEntity.Create(1, Utility.GenerateSeats(8, 8));

        Assert.NotNull(entity);
    }

    [Fact]
    public void Create_NotAllowed()
    {
        Assert.Throws<ArgumentException>(() => AuditoriumEntity.Create(1, new List<Seat>()));
    }

}