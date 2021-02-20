using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Produkty table
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Create new product to list of all products for transactions
        /// </summary>
        /// <param name="productDto">Object contain information for product</param>
        /// <param name="userId">Decoded token current login user id</param>
        /// <returns></returns>
        Task<bool> AddProductAsync(ProductDto productDto, int userId);

        /// <summary>
        /// Get product id by product name and seller
        /// </summary>
        /// <param name="productName">The name of the product</param>
        /// <param name="seller">The seller from product was bought</param>
        /// <returns>Product id</returns>
        ProductEntity FindProductByNameAndSeller( string productName, string seller );
    }
}
