using System;

namespace IdeaSpend.API
{
    /// <summary>
    /// The simplify object contain information of the <see cref="TransactionEntity"/>
    /// </summary>
    public class TransactionDto
    {
        #region Product Entity Properties

        /// <summary>
        /// The product name with pattern {nazwa_produktu} - {sprzedawca} from Produkty table
        /// </summary>
        public string ProductNameFrom { get; set; }

        #endregion

        #region Transaction Entity Properties

        /// <summary>
        /// The 3-letters code of currency
        /// </summary>
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public double Weights { get; set; }
        public double Paid { get; set; }
        public DateTime TransactionDate { get; set; }

        #endregion
    }
}