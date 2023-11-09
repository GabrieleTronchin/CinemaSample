using Cinema.Application.Commands;
using MediatR;

namespace Cinema.Application.Handlers
{
    public class SeatReservationCommandHandler : IRequestHandler<ReserveSeatCommand, ReserveSeatCommandComplete>
    {


        public SeatReservationCommandHandler()
        {

        }

        public Task<ReserveSeatCommandComplete> Handle(ReserveSeatCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}