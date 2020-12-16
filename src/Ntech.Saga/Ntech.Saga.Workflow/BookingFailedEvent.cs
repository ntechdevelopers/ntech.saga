using System;
using Ntech.Saga.Contracts;

namespace Ntech.Saga.Workflow
{
    public class BookingFailedEvent : IBookingFailedEvent
    {
        private readonly BookingSagaState _reportSagaState;
        public BookingFailedEvent(BookingSagaState reportSagaState)
        {
            _reportSagaState = reportSagaState;
        }

        public Guid CorrelationId => _reportSagaState.CorrelationId;

        public string CustomerId => _reportSagaState.CustomerId;
        public string BookingId => _reportSagaState.BookingId;
        public string FaultMessage => _reportSagaState.FaultMessage;
        public DateTime FaultTime => _reportSagaState.FaultTime;
    }
}
