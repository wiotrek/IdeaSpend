using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API
{
    [Table("Produkty")]
    public class ProductEntity
    {
        #region Primary Key

        [Key] public int ProductId { get; set; }

        #endregion

        #region Columns

        /// <summary>
        /// The name of products or services
        /// </summary>
        [Column("nazwa_produktu")]
        public string ProductName { get; set; }
        
        /// <summary>
        /// The source product bought from
        /// </summary>
        [Column("sprzedawca")]
        public string Seller { get; set; }
        
        /// <summary>
        /// The price in specific currency
        /// </summary>
        [Column("cena")] [Required]        
        public double Price { get; set; }
        
        /// <summary>
        /// The currency of the product user bought
        /// </summary>
        public string Currency { get; set; }
        
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

        #endregion

        #region Relations

        /// <summary>
        /// The foreign catalog id for Produkty table
        /// </summary>
        public int CatalogId { get; set; }
        
        /// <summary>
        /// One of many products are assign to specify catalog
        /// </summary>
        public CatalogEntity Catalog { get; set; }

        /// <summary>
        /// The foreign user id for Produkty table
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// One of many products are assign to specify user
        /// </summary>
        public UserEntity User { get; set; }

        #endregion
    }
}