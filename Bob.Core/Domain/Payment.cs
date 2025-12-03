using System;
using LinqToDB.Mapping;

namespace Bob.Core.Domain
{

    [Table("PAYMENT")]
    public sealed class Payment
    {
        public Payment() { }

        [PrimaryKey]
        [Column("PAYMENTID")]
        public int Id { get; set; }

        [Column("ORDERID")]
        public int OrderId { get; set; }

        [Column("PAYMENTDATE")]
        public DateTime PaymentDate { get; set; }

        [Column("PROCESSORID")]
        public int ProcessorId { get; set; }

        public Payment(int id, int orderId, DateTime paymentDate, int processorId)
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
        public int Id { get; set; }

        [Column("METHOD")]
        public string Method { get; set; }

        public PaymentProcessor(int id, string method)
        {
            Id = id;
            Method = method;
        }
    }
}
