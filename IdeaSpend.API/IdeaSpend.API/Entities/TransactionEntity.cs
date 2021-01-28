using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API
{
    /// <summary>
    /// The transaction entity with information about product/service
    /// </summary>
    [Table("Platnosci")]
    public class TransactionEntity
    {
        #region Primary Key

        [Key] public int TransactionId { get; set; }

        #endregion

        #region Columns

        /// <summary>
        /// The source user bought from
        /// </summary>
        [Column("sprzedawca")]
        public string Seller { get; set; }
        
        /// <summary>
        /// The name of products or services
        /// </summary>
        [Column("nazwa_produktu")]
        public string ProductName { get; set; }
        
        /// <summary>
        /// The price in pln currency
        /// </summary>
        [Column("cena")] [Required]        
        public double Price { get; set; }
        
        /// <summary>
        /// The quantity for products counted in pieces
        /// </summary>
        [Column("ilosc")]
        public int Quantity { get; set; }
        
        /// <summary>
        /// The product category like services, food, hygiene and so on
        /// </summary>
        [Column("kategoria")]
        public string Category { get; set; }
        
        /// <summary>
        /// The weight for products not counted in pieces
        /// </summary>
        [Column("waga")]
        public double Weights { get; set; }
        
        /// <summary>
        /// The unit weight like kg, g, dkg
        /// </summary>
        [Column("jednostka")]
        public string Unit { get; set; }
        
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
        /// One of many transactions are assigns to specify user
        /// </summary>
        public UserEntity User { get; set; }
        
        #endregion
    }
}