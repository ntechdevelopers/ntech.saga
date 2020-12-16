using System;

namespace Ntech.Saga.Contracts
{
    public interface IBookingRequestReceivedEvent
    {
        Guid CorrelationId { get; }
        string CustomerId { get; }
        string BookingId { get; }
        DateTime RequestTime { get; }
    }
}