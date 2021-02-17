using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaSpend.API
{
    /// <summary>
    /// The currency entity contain information about value currency on specific date
    /// NOTE: The source information is from https://free.currencyconverterapi.com/
    /// </summary>
    [Table("waluta")]
    public class CurrencyEntity
    {
        #region Primary Key

        [Key] public int CurrencyId { get; set; }

        #endregion

        #region Columns

        /// <summary>
        /// 3-letter short of the full name currency like PLN, EUR,...
        /// </summary>
        [Column("kod")]
        public string Code { get; set; }

        /// <summary>
        /// The value after conversion on PLN
        /// </summary>
        [Column("wartość")]
        public double Value { get; set; }

        /// <summary>
        /// Date when currency had this value
        /// </summary>
        [Column("data")]
        public DateTime Date { get; set; }

        #endregion
    }
}