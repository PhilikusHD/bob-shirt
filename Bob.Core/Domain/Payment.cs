using System;

namespace Bob.Core.Domain
{
    public readonly struct PaymentId
    {
        public uint Value { get; }
        public PaymentId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator PaymentId(uint id) => new PaymentId(id);
        public static implicit operator uint(PaymentId id) => id.Value;
    }

    public readonly struct ProcessorId
    {
        public uint Value { get; }
        public ProcessorId(uint value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator ProcessorId(uint value) => new ProcessorId(value);
        public static implicit operator uint(ProcessorId id) => id.Value;
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
