using System;

namespace Ntech.Saga.Contracts
{
    public interface IBookingCreatedEvent
    {
        Guid CorrelationId { get; }
        string CustomerId { get; }
        string BookingId { get; }
        string BlobUri { get; }
        DateTime CreationTime { get; }
    }
}
