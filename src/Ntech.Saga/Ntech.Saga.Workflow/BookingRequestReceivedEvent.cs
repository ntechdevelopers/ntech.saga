using System;
using Ntech.Saga.Contracts;

namespace Ntech.Saga.Workflow
{
    public class BookingRequestReceivedEvent : IBookingRequestReceivedEvent
    {
        private readonly BookingSagaState _reportSagaState;
        public BookingRequestReceivedEvent(BookingSagaState reportSagaState)
        {
            _reportSagaState = reportSagaState;
        }

        public Guid CorrelationId => _reportSagaState.CorrelationId;

        public string CustomerId => _reportSagaState.CustomerId;
        public string BookingId => _reportSagaState.BookingId;
        public DateTime RequestTime => _reportSagaState.RequestTime;
    }
}
