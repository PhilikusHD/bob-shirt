using System;
using LinqToDB.Mapping;

namespace Bob.Core.Domain
{


    [Table("[ORDER]")]
    public sealed class Order
    {
        public Order() { }

        [PrimaryKey]
        [Column("ORDERID")]
        public int Id { get; set; }

        [Column("CUSTOMERID")]
        public int CustomerId { get; set; }

        [Column("ORDERDATE")]
        public DateTime OrderDate { get; set; }

        [Column("TOTALAMOUNT")]
        public decimal TotalAmount { get; set; }

        public Order(int id, int customerId, DateTime orderDate, decimal totalAmount)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            TotalAmount = totalAmount;
        }
    }
}
