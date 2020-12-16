using MassTransit;
using Ntech.Saga.Contracts;
using System;
using System.Threading.Tasks;

namespace Ntech.Saga.Service.Handling.Consumers
{
    public class BookingCreatedConsumer : IConsumer<IBookingCreatedEvent>
    {
        public async Task Consume(ConsumeContext<IBookingCreatedEvent> context)
        {
            var bookingId = context.Message.BookingId;
            await Console.Out.WriteLineAsync($"Booking operation is succeeded! " +
                $"Booking Id: {bookingId}. " +
                $"Blob Uri: {context.Message.BlobUri}. " +
                $"Correlation Id: {context.Message.CorrelationId}");
            //Send mail, push notification, etc...
        }
    }
}
