using Cinema.Application.Payment.Events;
using MassTransit;

namespace Cinema.Application.Payment;

/// <summary>
/// just sample code
/// </summary>
public class PaymentStateMachine : MassTransitStateMachine<PaymentState>
{
    public PaymentStateMachine()
    {
        Event(() => PaymentRequested);
    }

    public Event<PaymentRequest> PaymentRequested { get; private set; }
}
