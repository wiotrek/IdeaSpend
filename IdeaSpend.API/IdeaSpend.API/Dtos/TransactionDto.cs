using System;

namespace IdeaSpend.API
{
    /// <summary>
    /// The simplify object contain information of the <see cref="TransactionEntity"/>
    /// </summary>
    public class TransactionDto
    {
        public string Seller { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }
    }
}