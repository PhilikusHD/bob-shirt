using System;

namespace Bob.Core.Domain
{
    public readonly struct PaymentId
    {
        public uint Value { get; }
        public PaymentId(uint value) => Value = value;

        public override string ToString() => Value.ToString();
    }

    public readonly struct ProcessorId
    {
        public uint Value { get; }
        public ProcessorId(uint value) => Value = value;

        public override string ToString() => Value.ToString();
    }

    public sealed class Payment
    {
        public PaymentId Id { get; }
        public OrderId OrderId { get; }
        public DateTime PaymentDate { get; }
        public ProcessorId ProcessorId { get; }

        public Payment(PaymentId id, OrderId orderId, DateTime paymentDate, ProcessorId processorId)
        {
            Id = id;
            OrderId = orderId;
            PaymentDate = paymentDate;
            ProcessorId = processorId;
        }
    }

    public sealed class PaymentProcessor
    {
        public ProcessorId Id { get; }
        public string Method { get; }

        public PaymentProcessor(ProcessorId id, string method)
        {
            Id = id;
            Method = method;
        }
    }
}
