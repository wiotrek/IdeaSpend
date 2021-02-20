using System.Threading.Tasks;
using System.Linq;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Produkty table
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        #region Private Members

        /// <summary>
        /// The scope application data context
        /// </summary>
        private readonly IdeaSpendContext _dataContext;

        private readonly ICatalogRepository _catalogRepository;

        #endregion

        #region Constructor

        public ProductRepository(IdeaSpendContext dataContext, ICatalogRepository catalogRepository)
        {
            _dataContext = dataContext;
            _catalogRepository = catalogRepository;
        }

        #endregion

        /// <summary>
        /// Create new product to list of all products for transactions
        /// </summary>
        /// <param name="productDto">Object contain information for product</param>
        /// <param name="userId">Decoded token current login user id</param>
        /// <returns></returns>
        public async Task<bool> AddProductAsync(ProductDto productDto, int userId)
        {
            var catalogId = default(int);

            // If user choose any catalog then get id of the catalog
            if (!string.IsNullOrWhiteSpace(productDto.CatalogName))
                catalogId = _catalogRepository.FindCatalogIdByName(productDto.CatalogName);

            // Create product to add
            var product = new ProductEntity
            {
                ProductName = productDto.ProductName,
                Seller = productDto.Seller,
                Price = productDto.Price,
                Unit = productDto.Unit,
                CatalogId = catalogId,
                UserId = userId
            };

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
        
    }
}
