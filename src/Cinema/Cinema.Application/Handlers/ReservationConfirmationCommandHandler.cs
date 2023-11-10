using Cinema.Application.Commands;
using Cinema.Persistence.Repositories;
using Cinema.Persistence.Repositories.Abstractions;
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