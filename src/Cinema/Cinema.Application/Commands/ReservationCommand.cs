using MediatR;

namespace Cinema.Application.Commands;

public class ReservationCommand : IRequest<ReservationComplete>
{
    public Guid Id { get; set; }
    public Guid ShowtimeId { get; set; }
    public IEnumerable<ReservationSeat> seats { get; set; }
    public int AuditoriumId { get; set; }
}

public record ReservationSeat
{
    public short SeatsNumber { get; set; }
    public short Row { get; set; }
}

public record ReservationComplete
{
    public Guid Id { get; set; }

    public IEnumerable<ReservationSeat> SeatsNumber { get; set; }

    public int AuditoriumId { get; set; }

    public string MovieTitle { get; set; }
}
