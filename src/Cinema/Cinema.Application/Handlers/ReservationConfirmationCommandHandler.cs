using Cinema.Application.Commands;
using Cinema.Domain.Ticket.Repository;
using MediatR;

namespace Cinema.Application.Handlers
{
    public class ReservationConfirmationCommandHandler : IRequestHandler<ReservationConfirmationCommand, ReservationConfirmationComplete>
    {
        private readonly ITicketsRepository _ticketsRepository;

        public ReservationConfirmationCommandHandler(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
        }

        public async Task<ReservationConfirmationComplete> Handle(ReservationConfirmationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket2Confirm = await _ticketsRepository.GetAsync(request.ReservationId, cancellationToken);

                //Add  saga pattern

                //-We will need the GUID of the reservation, it is only possible to do it while the seats are reserved.
                //- It is not possible to buy two times the same seat.
                //-We are not going to use a Payment abstraction for this case, just have an Endpoint which I can Confirm a Reservation.

                await _ticketsRepository.ConfirmPaymentAsync(ticket2Confirm, cancellationToken);

                return new ReservationConfirmationComplete() { Success = true };
            }
            catch (Exception ex)
            {
                //add logs
                return new ReservationConfirmationComplete() { Success = false };
            }

        }
    }
}