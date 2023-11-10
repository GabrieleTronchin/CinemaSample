namespace Cinema.Application.Queries.Auditorium.Models
{
    public class SeatReadModel
    {
        public short Row { get; set; }
        public short SeatNumber { get; set; }

        public SeatStatus SeatStatus { get; set;}
    }

    public enum SeatStatus
    {
       Free,
       Reserved,
       Sold
    }
}
