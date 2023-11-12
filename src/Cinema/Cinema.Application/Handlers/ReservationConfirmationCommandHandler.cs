using Cinema.Application.Commands;
using Cinema.Domain.Primitives;
using Cinema.Domain.Ticket.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Handlers
{
    public class ReservationConfirmationCommandHandler : IRequestHandler<ReservationConfirmationCommand, ReservationConfirmationComplete>
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly ILogger<ReservationConfirmationCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationConfirmationCommandHandler(
            ILogger<ReservationConfirmationCommandHandler> logger,
            ITicketsRepository ticketsRepository,
            IUnitOfWork unitOfWork)
        {
            _ticketsRepository = ticketsRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationConfirmationComplete> Handle(ReservationConfirmationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ticket = await _ticketsRepository.GetAsync(request.ReservationId, cancellationToken);

                //TODO Add polly direct call?! BS bus saga?!

                ticket.ConfirmPayment();

                _unitOfWork.Commit();
                return new ReservationConfirmationComplete() { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred completing payment.");
                return new ReservationConfirmationComplete() { Success = false };
            }

        }
    }
}