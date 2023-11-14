using Cinema.Application.Payment.Events;
using MassTransit;

namespace Cinema.Application.Payment;

/// <summary>
/// Just a sample for conceptual request
/// https://medium.com/adessoturkey/saga-state-machine-masstransit-automatonymous-request-response-pattern-10f14603964
/// Orchestration-based Saga
/// </summary>
public class PaymentSagaDefinition : SagaDefinition<PaymentState>
{
    public PaymentSagaDefinition()
    {
        // specify the message limit at the endpoint level, which influences
        // the endpoint prefetch count, if supported
        Endpoint(e => e.ConcurrentMessageLimit = 16);
    }

    /// <summary>
    /// Sample idea taken from MassTransit site
    /// </summary>
    /// <param name="endpointConfigurator"></param>
    /// <param name="sagaConfigurator"></param>
    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<PaymentState> sagaConfigurator)
    {
        var partition = endpointConfigurator.CreatePartitioner(16);
        sagaConfigurator.Message<PaymentRequest>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
        sagaConfigurator.Message<PaymentCompleted>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
        sagaConfigurator.Message<PaymentCancelled>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
    }
}
