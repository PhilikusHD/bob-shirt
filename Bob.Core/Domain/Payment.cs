using System;
using LinqToDB.Mapping;

namespace Bob.Core.Domain
{
    public readonly struct PaymentId
    {
        public int Value { get; }
        public PaymentId(int value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator PaymentId(int id) => new PaymentId(id);
        public static implicit operator int(PaymentId id) => id.Value;
    }

    public readonly struct ProcessorId
    {
        public int Value { get; }
        public ProcessorId(int value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator ProcessorId(int value) => new ProcessorId(value);
        public static implicit operator int(ProcessorId id) => id.Value;
    }

    [Table("PAYMENT")]
    public sealed class Payment
    {
        public Payment() { }

        [PrimaryKey]
        [Column("PAYMENTID")]
        public PaymentId Id { get; set; }

        [Column("ORDERID")]
        public OrderId OrderId { get; set; }

        [Column("PAYMENTDATE")]
        public DateTime PaymentDate { get; set; }

        [Column("PROCESSORID")]
        public ProcessorId ProcessorId { get; set; }

        public Payment(PaymentId id, OrderId orderId, DateTime paymentDate, ProcessorId processorId)
        {
            Id = id;
            OrderId = orderId;
            PaymentDate = paymentDate;
            ProcessorId = processorId;
        }
    }

    [Table("PAYMENTPROCESSOR")]
    public sealed class PaymentProcessor
    {
        public PaymentProcessor() { }

        [PrimaryKey, Identity]
        [Column("PROCESSORID")]
        public ProcessorId Id { get; set; }

        [Column("METHOD")]
        public string Method { get; set; }

        public PaymentProcessor(ProcessorId id, string method)
        {
            Id = id;
            Method = method;
        }
    }
}
