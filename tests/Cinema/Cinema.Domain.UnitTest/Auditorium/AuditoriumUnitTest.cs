using Cinema.Domain.AuditoriumDefinition;

namespace Cinema.Domain.UnitTest.Auditorium;

public class AuditoriumUnitTest
{
    [Fact]
    public void Create()
    {

        var entity = AuditoriumEntity.Create(1, Utility.GenerateSeats(8, 8));

        Assert.NotNull(entity);
        Assert.NotEqual(Guid.Empty.ToString(), entity.Id.ToString());
        Assert.NotEmpty(entity.Seats);
    }

    [Fact]
    public void Create_NotAllowed()
    {
        var ex = Assert.Throws<ArgumentException>(() => AuditoriumEntity.Create(1, new List<Seat>()));
        Assert.Equal("An Auditorium should contains seats", ex.Message);
    }

}