using System;

namespace Ntech.Saga.Contracts
{
    public interface IBookingCancelledEvent
    {
        Guid CorrelationId { get; }
        string CustomerId { get; }
        string BookingId { get; }
        DateTime CancelledTime { get; }
    }
}
