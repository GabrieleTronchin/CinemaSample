using Cinema.Domain.Ticket;

namespace Cinema.Domain.UnitTest.Ticket;

public class TicketUnitTest
{
    [Fact]
    public void CreateTicketTest()
    {

        var entity = TicketEntity.Create(Utility.GenerateSeats(28, 22), Guid.NewGuid(), "My Test Movie");

        Assert.NotNull(entity);
        Assert.NotEqual(Guid.Empty.ToString(), entity.Id.ToString());
        Assert.NotEmpty(entity.Seats);
        Assert.Equal("My Test Movie", entity.MovieTitle);
        Assert.False(entity.Paid);
    }

    [Fact]
    public void CreateTicketTest_NotAllowed()
    {
        var ex = Assert.Throws<ArgumentException>(() => TicketEntity.Create(new List<Seat>(), Guid.NewGuid(), "My Test Movie"));
        Assert.NotEmpty(ex.Message);
        Assert.Equal("Invalid seats", ex.Message);
    }

    //just a sample of Theory
    [Theory]
    [InlineData("     ")]
    [InlineData("")]
    [InlineData(null)]
    public void CreateTicketTest_NotAllowedForMovieTitle(string movieTitle)
    {
        var ex = Assert.Throws<ArgumentException>(() => TicketEntity.Create(Utility.GenerateSeats(28, 22), Guid.NewGuid(), movieTitle));
        Assert.NotEmpty(ex.Message);
        Assert.Equal("Invalid movieTile", ex.Message);
    }
}