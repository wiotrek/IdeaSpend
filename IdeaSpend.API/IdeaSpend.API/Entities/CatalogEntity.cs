using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API
{
    /// <summary>
    /// The catalog contains products
    /// </summary>
    [Table("Katalog_produktów")]
    public class CatalogEntity
    {
        #region Primary Key

        [Key] public int CatalogId { get; private set; }

        #endregion
        
        #region Columns

        /// <summary>
        /// The catalog contain products
        /// </summary>
        [Column("nazwa")]
        public string CatalogName { get; set; }
        
        #endregion

        #region Relations

        /// <summary>
        /// The specific catalog contain many products
        /// </summary>
        public ICollection<ProductEntity> Produts { get; set; }

        /// <summary>
        /// The foreign user id for Katalog_produktów table
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// One of many catalogs are assign to specify user
        /// </summary>
        public UserEntity User { get; set; }

        #endregion
    }
}