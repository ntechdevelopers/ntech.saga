using System;

namespace Ntech.Saga.Contracts
{
    public interface IBookingFailedEvent
    {
        Guid CorrelationId { get; }
        string CustomerId { get; }
        string BookingId { get; }
        string FaultMessage { get; }
        DateTime FaultTime { get; }
    }
}
