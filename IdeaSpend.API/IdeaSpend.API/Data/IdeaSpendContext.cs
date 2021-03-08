using Microsoft.EntityFrameworkCore;

namespace IdeaSpend.API
{
    /// <summary>
    /// The database representational model for application
    /// </summary>
    public class IdeaSpendContext : DbContext
    {
        #region Public Properties

        /// <summary>
        /// The settings for the application
        /// </summary>
        public DbSet<UserEntity> Users { get; set; } 
        public DbSet<TransactionEntity> Transactions { get; set; } 
        public DbSet<ProductEntity> Products { get; set; } 
        public DbSet<CatalogEntity> Catalogs { get; set; } 
        public DbSet<CurrencyEntity> Currency { get; set; } 

        #endregion
        
        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public IdeaSpendContext(DbContextOptions<IdeaSpendContext> options) : base(options) {}

        #endregion
    }
}