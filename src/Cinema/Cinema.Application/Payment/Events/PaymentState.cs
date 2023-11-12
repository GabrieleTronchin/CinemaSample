using MassTransit;

namespace Cinema.Application.Payment.Events;

public class PaymentState : SagaStateMachineInstance
{
    public Guid CorrelationId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool IsCompleted { get => true; }
}
