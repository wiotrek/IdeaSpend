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