using Ntech.Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ntech.Saga.Service.Api.Commands
{
    public class BookingRequestCommand : IBookingRequestCommand
    {
        public string CustomerId { get; set; }
        public string BookingId { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
