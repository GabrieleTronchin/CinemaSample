using Cinema.Application.Commands;
using MediatR;

namespace Cinema.Application.Handlers
{
    public class ConfirmReservationCommandHandler : IRequestHandler<ReserveSeatCommand, ReserveSeatCommandComplete>
    {


        public ConfirmReservationCommandHandler()
        {

        }

        public async Task<ReserveSeatCommandComplete> Handle(ReserveSeatCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}