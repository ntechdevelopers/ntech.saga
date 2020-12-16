using MassTransit;
using Ntech.Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ntech.Saga.Service.Handling.Consumers
{
    public class BookingRequestReceivedConsumer : IConsumer<IBookingRequestReceivedEvent>
    {
        public async Task Consume(ConsumeContext<IBookingRequestReceivedEvent> context)
        {
            Console.WriteLine($"Booking request is received...");
            var bookingId = context.Message.BookingId;

            await Console.Out.WriteLineAsync($"Booking request is received. " +
                $"Booking Id: {bookingId}." +
                $"Correlation Id: {context.Message.CorrelationId}");
            //Get report from Db, file, etc...
            if (bookingId.StartsWith("booking", StringComparison.Ordinal))
            {
                await context.Publish<IBookingCreatedEvent>(new
                {
                    context.Message.CorrelationId,
                    context.Message.CustomerId,
                    context.Message.BookingId,
                    BlobUri = "https://google.com",
                    CreationTime = DateTime.Now
                });
            }
            else if (bookingId.StartsWith("cancel", StringComparison.Ordinal))
            {
                await context.Publish<IBookingCancelledEvent>(new
                {
                    context.Message.CorrelationId,
                    context.Message.CustomerId,
                    context.Message.BookingId,
                    CancelledTime = DateTime.Now
                });
            }
            else
            {
                await context.Publish<IBookingFailedEvent>(new
                {
                    context.Message.CorrelationId,
                    context.Message.CustomerId,
                    context.Message.BookingId,
                    FaultMessage = "Booking name is invalid! Please retry again!",
                    FaultTime = DateTime.Now
                });
            }
        }
    }
}
