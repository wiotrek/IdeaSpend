using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Get all products which user have with catalogs to which product are assigned
        /// </summary>
        public IEnumerable<ProductEntity> GetUserProducts(int userId)
        {
            return _dataContext.Products
                    
                // Join Catalog table to get access catalog content
                .Include(c => c.Catalog)
                    
                .Where(u => u.UserId == userId)
                .ToList();
        }

        public IEnumerable<ProductEntity> GetUserProductsByCatalogId( int userId, int? catalogId )
        {
            return _dataContext.Products
                .Where(i => i.CatalogId == catalogId)
                .Where(i => i.UserId == userId)
                .ToList();
        }
        

        public bool DeleteProduct(int userId, string productName) 
        {
            var foundEntity = _dataContext.Products
                .Where(i => i.UserId == userId)
                .SingleOrDefault(x => x.ProductName == productName);

            if (foundEntity == null)
                return false;
            
            _dataContext.Products.Remove(foundEntity);
            return _dataContext.SaveChanges() > 0;
        }
        

        #endregion
    }
}
