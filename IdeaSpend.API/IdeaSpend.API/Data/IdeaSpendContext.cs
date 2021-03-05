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

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            // modelBuilder.Entity<CatalogEntity>()
            //     .HasMany ( p => p.Produts )
            //     .WithOne ( c => c.Catalog )
            //     .IsRequired ( false )
            //     .OnDelete ( DeleteBehavior.SetNull );
            //
            // modelBuilder.Entity<TransactionEntity>()
            //     .HasOne ( t => t.Product )
            //     .WithOne ( p => p.Transaction );
            //
            // modelBuilder.Entity<TransactionEntity>()
            //     .HasOne ( t => t.User )
            //     .WithMany ( p => p.Transactions );
            //
            // modelBuilder.Entity<ProductEntity>()
            //     .HasOne ( t => t.User )
            //     .WithMany ( p => p.Products );
            //
            // modelBuilder.Entity<ProductEntity>()
            //     .HasOne( t => t.Catalog )
            //     .WithMany ( p => p.Produts );
            //
            // modelBuilder.Entity<UserEntity>()
            //     .HasMany ( c => c.Catalogs )
            //     .WithOne ( u => u.User );
            //
            // modelBuilder.Entity<UserEntity>()
            //     .HasMany ( c => c.Products )
            //     .WithOne ( u => u.User );
            //
            // modelBuilder.Entity<UserEntity>()
            //     .HasMany ( c => c.Transactions )
            //     .WithOne ( u => u.User );
        }
    }
}