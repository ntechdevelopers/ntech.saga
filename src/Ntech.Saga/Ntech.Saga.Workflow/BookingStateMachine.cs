using System;
using Automatonymous;
using Ntech.Saga.Contracts;

namespace Ntech.Saga.Workflow
{

    public class BookingStateMachine : MassTransitStateMachine<BookingSagaState>
    {
        public State Submitted { get; private set; }
        public State Processed { get; private set; }
        public State Cancelled { get; private set; }

        public Event<IBookingRequestCommand> CreateBookingCommandReceived { get; private set; }
        public Event<IBookingRequestReceivedEvent> BookingRequestReceived { get; private set; }

        public Event<IBookingCancelledEvent> BookingCancelled { get; private set; }
        public Event<IBookingCreatedEvent> BookingCreated { get; private set; }
        public Event<IBookingFailedEvent> BookingFailed { get; private set; }
       

        public BookingStateMachine()
        {
            Console.WriteLine("BookingStateMachine...");
            InstanceState(x => x.CurrentState);

            Event(() => CreateBookingCommandReceived, cc => cc.CorrelateBy(state => state.BookingId, context => context.Message.BookingId).SelectId(context => Guid.NewGuid()));
            Event(() => BookingRequestReceived, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => BookingCancelled, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => BookingCreated, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => BookingFailed, x => x.CorrelateById(context => context.Message.CorrelationId));

            During(Initial,
                When(CreateBookingCommandReceived).Then(context =>
                {
                    Console.WriteLine("************************ Initial => Submitted ************************");
                    context.Instance.CustomerId = context.Data.CustomerId;
                    context.Instance.RequestTime = context.Data.RequestTime;
                    context.Instance.BookingId = context.Data.BookingId;
                })
                .Publish(ctx => new BookingRequestReceivedEvent(ctx.Instance))
                .TransitionTo(Submitted)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
            );

            During(Submitted,
                When(BookingRequestReceived).Then(context =>
                {
                    Console.WriteLine("*********************** Submitted => Processed ***********************");
                })
                .TransitionTo(Processed)
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString())));

            During(Processed,
                Ignore(BookingCancelled),
                When(BookingCreated).Then(context =>
                {
                    Console.WriteLine("************************ Processed => Final ************************");
                    context.Instance.CustomerId = context.Data.CustomerId;
                    context.Instance.BookingId = context.Data.BookingId;

                    context.Instance.BlobUri = context.Data.BlobUri;
                    context.Instance.CreationTime = context.Data.CreationTime;
                })
                .Publish(ctx => new BookingCreatedEvent(ctx.Instance)).Finalize()
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString())),

                When(BookingFailed).Then(context =>
                {
                    context.Instance.CustomerId = context.Data.CustomerId;
                    context.Instance.BookingId = context.Data.BookingId;

                    context.Instance.FaultMessage = context.Data.FaultMessage;
                    context.Instance.FaultTime = context.Data.FaultTime; 
                })
                .Publish(ctx => new BookingFailedEvent(ctx.Instance)).Finalize()
                .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
            );

            During(Processed,
                 Ignore(BookingCreated),
                 When(BookingCancelled).Then(context =>
                 {
                     Console.WriteLine("*********************** Processed => Cancelled ***********************");
                 })
                 .TransitionTo(Cancelled)
                 .Publish(ctx => new BookingRequestCancelledEvent(ctx.Instance))
                 .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString())),

                 When(BookingFailed).Then(context =>
                 {
                     context.Instance.CustomerId = context.Data.CustomerId;
                     context.Instance.BookingId = context.Data.BookingId;

                     context.Instance.FaultMessage = context.Data.FaultMessage;
                     context.Instance.FaultTime = context.Data.FaultTime;

                 })
                 .Publish(ctx => new BookingFailedEvent(ctx.Instance)).Finalize()
                 .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString())));

            During(Cancelled,
                 Ignore(CreateBookingCommandReceived),
                 Ignore(BookingRequestReceived),
                 When(BookingRequestReceived).Then(context =>
                 {
                     Console.WriteLine("*********************** Cancelled => Final ***********************");
                 })
                 .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString())));

            SetCompletedWhenFinalized();
        }
    }
}
