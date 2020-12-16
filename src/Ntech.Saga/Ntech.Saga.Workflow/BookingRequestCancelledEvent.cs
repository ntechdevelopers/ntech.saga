using System;
using Ntech.Saga.Contracts;

namespace Ntech.Saga.Workflow
{
    public class BookingRequestCancelledEvent : IBookingCancelledEvent
    {
        private readonly BookingSagaState _reportSagaState;
        public BookingRequestCancelledEvent(BookingSagaState reportSagaState)
        {
            _reportSagaState = reportSagaState;
        }

        public Guid CorrelationId => _reportSagaState.CorrelationId;

        public string CustomerId => _reportSagaState.CustomerId;
        public string BookingId => _reportSagaState.BookingId;
        public DateTime CancelledTime => _reportSagaState.RequestTime;
    }
}
