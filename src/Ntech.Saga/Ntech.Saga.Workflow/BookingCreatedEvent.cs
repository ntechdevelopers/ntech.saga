using System;
using Ntech.Saga.Contracts;

namespace Ntech.Saga.Workflow
{
    public class BookingCreatedEvent : IBookingCreatedEvent
    {
        private readonly BookingSagaState _reportSagaState;
        public BookingCreatedEvent(BookingSagaState reportSagaState)
        {
            _reportSagaState = reportSagaState;
        }

        public Guid CorrelationId => _reportSagaState.CorrelationId;

        public string CustomerId => _reportSagaState.CustomerId;
        public string BookingId => _reportSagaState.BookingId;

        public string BlobUri => _reportSagaState.BlobUri;

        public DateTime CreationTime => _reportSagaState.CreationTime;
    }
}
