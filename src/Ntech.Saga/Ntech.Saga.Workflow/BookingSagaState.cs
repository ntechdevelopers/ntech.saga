using System;
using System.Text;
using Automatonymous;

namespace Ntech.Saga.Workflow
{
    public class BookingSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public State CurrentState { get; set; }
        public int Version { get; set; }


        public string CustomerId { get; set; }
        public string BookingId { get; set; }

        public DateTime RequestTime { get; set; }

        public string BlobUri { get; set; }
        public DateTime CreationTime { get; set; }

        public string FaultMessage { get; set; }
        public DateTime FaultTime { get; set; }

        public override string ToString()
        {
            var properties = GetType().GetProperties();
            var sb = new StringBuilder();
            foreach (var info in properties)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value);
            }
            return sb.ToString();
        }
    }
}
