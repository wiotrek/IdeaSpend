using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API.Models
{
    /// <summary>
    /// An user entity model represent personal user details
    /// </summary>
    [Table("Uzytkownik")]
    public class User
    {
        #region Primary Key

        [Key]                              public int UserId { get; set; }

        #endregion
        
        #region Columns

        [Column("imie")]              public string FirstName { get; set; }
        [Column("nazwisko")]          public string LastName { get; set; }
        [Column("nazwa_uzytkownika")] public string Username { get; set; }
        [Column("email")]             public string Email { get; set; }
        [Column("salt")]              public byte[] PasswordSalt { get; set; }
        [Column("hash")]              public byte[] PasswordHash { get; set; }
        [Column("account_created")]   public DateTime Created { get; set; }
        [Column("last_login")]        public DateTime LastLogin { get; set; }

        #endregion
    }
}