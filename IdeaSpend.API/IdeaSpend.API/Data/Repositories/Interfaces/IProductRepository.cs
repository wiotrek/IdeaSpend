using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Produkty table
    /// </summary>
    public interface IProductRepository : IBaseRepository
    {
        /// <summary>
        /// Create new product to list of all products for transactions
        /// </summary>
        /// <param name="product">Object contain information for product</param>
        /// <returns></returns>
        Task<bool> AddProductAsync(ProductEntity product);

        /// <summary>
        /// Get product id by product name and seller
        /// </summary>
        /// <param name="productName">The name of the product</param>
        /// <param name="seller">The seller from product was bought</param>
        /// <returns>Product id</returns>
        ProductEntity FindProductByNameAndSeller( string productName, string seller );

        /// <summary>
        /// Get all products which user have with catalogs to which product are assigned
        /// </summary>
        IEnumerable<ProductEntity> GetUserProducts(int userId);

        IEnumerable<ProductEntity> GetUserProductsByCatalogId( int userId, int? catalogId );
    }
}
