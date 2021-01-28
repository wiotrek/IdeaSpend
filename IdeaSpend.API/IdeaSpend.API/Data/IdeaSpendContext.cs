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

        #endregion
        
        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public IdeaSpendContext(DbContextOptions<IdeaSpendContext> options) : base(options) {}

        #endregion

        #region Protected Overrides Methods
        
        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            // configure one-to-many relationship
            modelBuilder.Entity<UserEntity>()
                .OwnsMany ( t => t.Transactions )
                .HasKey ( u => u.TransactionId );
        }

        #endregion
        
    }
}