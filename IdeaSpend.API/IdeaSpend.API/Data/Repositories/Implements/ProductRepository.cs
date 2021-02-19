using System.Threading.Tasks;

namespace IdeaSpend.API
{
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
            int catalogId = default(int);

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
    }
}
