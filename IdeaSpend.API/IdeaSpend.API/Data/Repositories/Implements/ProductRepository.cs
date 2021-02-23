using System.Threading.Tasks;
using System.Linq;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Produkty table
    /// </summary>
    public class ProductRepository : BaseRepository, IProductRepository
    {
        #region Constructor

        public ProductRepository(IdeaSpendContext dataContext) : base(dataContext) { }

        #endregion

        #region Implemented Methods

        /// <summary>
        /// Save new product to db
        /// </summary>
        public async Task<bool> AddProductAsync(ProductEntity product)
        {
            await _dataContext.AddAsync(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Get product id by product name and seller
        /// </summary>
        /// <param name="productName">The name of the product</param>
        /// <param name="seller">The seller from product was bought</param>
        /// <returns>Product id</returns>
        public ProductEntity FindProductByNameAndSeller( string productName, string seller )
        {
            // Go to Product table
            var product = _dataContext.Products

                // Filter by product name
                .Where ( p => p.ProductName == productName )

                // and by seller and return product id
                .FirstOrDefault ( s => s.Seller == seller );
            
            return product;
        }

        #endregion
    }
}
