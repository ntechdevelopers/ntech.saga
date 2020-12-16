using MassTransit;
using Ntech.Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ntech.Saga.Service.Handling.Consumers
{
    public class BookingFailedConsumer : IConsumer<IBookingFailedEvent>
    {
        public async Task Consume(ConsumeContext<IBookingFailedEvent> context)
        {
            var bookingId = context.Message.BookingId;
            await Console.Out.WriteLineAsync($"Booking operation is failed! " +
                $"Booking Id: {bookingId}. " +
                $"Fault Message: {context.Message.FaultMessage}. " +
                $"Correlation Id: {context.Message.CorrelationId}");
            //Send mail, push notification, etc...
        }
    }
}
