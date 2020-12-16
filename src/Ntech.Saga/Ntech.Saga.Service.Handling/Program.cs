using MassTransit;
using Ntech.Saga.Contracts;
using Ntech.Saga.Service.Handling.Consumers;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Ntech.Saga.Service.Handling
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // Register endpoint handle registerorder
            var busBooking = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                Console.WriteLine("Register endpoint handle registerorder...");
                cfg.ReceiveEndpoint(host, RabbitMqConstants.BookingRequestServiceQueue, e =>
                {
                    e.Consumer<BookingRequestReceivedConsumer>();
                });
            });

            // Register endpoint handle event successfully
            var busNotifySuccessfully = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                Console.WriteLine("Register endpoint handle event successfully...");
                cfg.ReceiveEndpoint(host, RabbitMqConstants.BookingRequestServiceQueue, e =>
                {
                    e.Consumer<BookingCreatedConsumer>();
                });
            });

            // Register endpoint handling event failure
            var busNotifyFailure = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                Console.WriteLine("Register endpoint handling event failure...");
                cfg.ReceiveEndpoint(host, RabbitMqConstants.BookingRequestServiceQueue, e =>
                {
                    e.Consumer<BookingFailedConsumer>();
                });
            });

            await busBooking.StartAsync(CancellationToken.None);
            await busNotifySuccessfully.StartAsync(CancellationToken.None);
            await busNotifyFailure.StartAsync(CancellationToken.None);
            Console.WriteLine("Listening for report requests.. Press enter to exit");
            Console.ReadLine();
            await busBooking.StopAsync(CancellationToken.None);
            await busNotifySuccessfully.StopAsync(CancellationToken.None);
            await busNotifyFailure.StopAsync(CancellationToken.None);
        }
    }
}
