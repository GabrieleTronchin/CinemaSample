using Cinema.Application.Payment.Events;
using Cinema.Application.Ticket.Commands;
using Cinema.Domain.Ticket.Repository;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Ticket;

public class ReservationConfirmationCommandHandler : IRequestHandler<ReservationConfirmationCommand, ReservationConfirmationComplete>
{
    private readonly ITicketsRepository _ticketsRepository;
    private readonly ILogger<ReservationConfirmationCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    public ReservationConfirmationCommandHandler(
        ILogger<ReservationConfirmationCommandHandler> logger,
        ITicketsRepository ticketsRepository,
        IPublishEndpoint publishEndpoint)
    {
        _ticketsRepository = ticketsRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ReservationConfirmationComplete> Handle(ReservationConfirmationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ticket = await _ticketsRepository.GetAsync(request.ReservationId, cancellationToken);

            // Requirement asking for a direct call to payment service, but in order to have more decupled code is better a MassTransit event.
            // A draft of Payment saga has been added to the solution just as a sample idea, payment code in the future may be in a separate service.
            // For this demo, i just publish staring event and set payment as completed.
            await _publishEndpoint.Publish(new PaymentRequest());
            ticket.ConfirmPayment();

            await _ticketsRepository.SaveChangesAsync();

            return new ReservationConfirmationComplete() { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred completing payment.");
            throw;
        }

    }
}