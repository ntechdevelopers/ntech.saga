using MassTransit;
using MassTransit.Saga;
using Ntech.Saga.Contracts;
using Ntech.Saga.Workflow;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ntech.Saga.Service.Management
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var sagaStateMachine = new BookingStateMachine();
            var repository = new InMemorySagaRepository<BookingSagaState>();

            // Register endpoint handle saga
            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                Console.WriteLine("Register endpoint handle saga...");
                cfg.ReceiveEndpoint(host, RabbitMqConstants.SagaQueue, e =>
                {
                    e.StateMachineSaga(sagaStateMachine, repository);
                });
            });

            await bus.StartAsync(CancellationToken.None);
            Console.WriteLine("Saga active.. Press enter to exit");
            Console.ReadLine();
            await bus.StopAsync(CancellationToken.None);
        }
    }
}
