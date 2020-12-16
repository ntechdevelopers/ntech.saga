using System;

namespace Ntech.Saga.Contracts
{
    public interface IBookingRequestCommand
    {
        string CustomerId { get; set; }
        string BookingId { get; set; }
        DateTime RequestTime { get; set; }
    }
}
