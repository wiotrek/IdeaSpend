using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API
{
    [Table("Platnosci")]
    public class TransactionEntity
    {
        #region Primary Key

        [Key] public int TransactionId { get; set; }

        #endregion

        #region Columns

        /// <summary>
        /// The quantity for products counted in pieces
        /// </summary>
        [Column("ilosc")]
        public int Quantity { get; set; }

        /// <summary>
        /// The transaction date indicates when products was bought
        /// </summary>
        [Column("data_transakcji")] 
        public DateTime TransactionDate { get; set; }
        
        /// <summary>
        /// The currency of the product user bought
        /// </summary>
        [Column("waluta")]
        public string Currency { get; set; }
        
        /// <summary>
        /// The weight for products not counted in pieces
        /// </summary>
        [Column("waga")]
        public double Weights { get; set; }

        /// <summary>
        /// A total cost for single transaction
        /// </summary>
        [Column("zapłacono")]
        public double Paid { get; set; }
        
        #endregion
        
        #region Relations

        /// <summary>
        /// The foreign user id
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// One of many transactions are assign to specify user
        /// </summary>
        public UserEntity User { get; set; }
        
        /// <summary>
        /// The foreign product id
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// One of many transactions are assign to specify product
        /// </summary>
        public ProductEntity Product { get; set; }

        #endregion
    }
}