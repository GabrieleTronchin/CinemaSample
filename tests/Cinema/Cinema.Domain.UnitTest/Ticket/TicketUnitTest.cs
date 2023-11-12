using Bogus;
using Cinema.Domain.Ticket;

namespace Cinema.Domain.UnitTest.Ticket
{
    public class TicketUnitTest
    {
        [Fact]
        public void CreateTicketTest()
        {

            var ticket = TicketEntity.Create(Utility.GenerateSeats(28, 22), Guid.NewGuid(), "My Test Movie");

            Assert.NotNull(ticket);
        }

        [Fact]
        public void CreateTicketTest_NotAllowed()
        {
           Assert.Throws<ArgumentException>(() => TicketEntity.Create(new List<Seat>(), Guid.NewGuid(), "My Test Movie"));
        }

    }
}