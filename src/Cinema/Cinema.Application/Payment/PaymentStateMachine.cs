using Cinema.Application.Payment.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Payment;
/// <summary>
/// just sample code
/// </summary>
public class PaymentStateMachine :
    MassTransitStateMachine<PaymentState>
{
    public PaymentStateMachine()
    {
        Event(() => PaymentRequested);
    }

    public Event<PaymentRequest> PaymentRequested { get; private set; }
}
