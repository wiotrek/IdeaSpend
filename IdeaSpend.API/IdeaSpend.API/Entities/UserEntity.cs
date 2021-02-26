using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API
{
    /// <summary>
    /// An user entity model represent personal user details
    /// </summary>
    [Table("Uzytkownik")]
    public class UserEntity
    {
        #region Primary Key

        [Key] public int UserId { get; set; }

        #endregion
        
        #region Columns

        [Required]
        [Column("imie")] [StringLength(30)]              
        public string FirstName { get; set; }
        
        [Required] [StringLength(30)]
        [Column("nazwisko")]          
        public string LastName { get; set; }
        
        [Required] [StringLength(30)]
        [Column("nazwa_uzytkownika")] 
        public string Username { get; set; }
        
        [Required]
        [Column("email")]             
        public string Email { get; set; }
        
        [Column("salt")]              
        public byte[] PasswordSalt { get; set; }
        
        [Column("hash")]              
        public byte[] PasswordHash { get; set; }
        
        [Column("utworzono")]   
        public DateTime Created { get; set; }
        
        // TODO: change last login to last active
        [Column("ostatnie_logowanie")]        
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// It's a total amount of the income from different source
        /// </summary>
        [Column("dochód")]
        public double Income { get; set; }

        #endregion
        
        #region Relations

        /// <summary>
        /// The specify user have many transactions
        /// </summary>
        public ICollection<TransactionEntity> Transactions { get; set; }
        
        /// <summary>
        /// The specify user have many products
        /// </summary>
        public ICollection<ProductEntity> Products { get; set; }

        /// <summary>
        /// The specify user have many catalogs
        /// </summary>
        public ICollection<CatalogEntity> Catalogs { get; set; }
        
        #endregion
    }
}