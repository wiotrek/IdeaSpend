using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public class ProductService {

        #region Private Members

        private readonly IProductRepository _productRepository;
        private readonly ICatalogRepository _catalogRepository;

        #endregion
        
        #region Constructor

        public ProductService(IProductRepository productRepository,
                              ICatalogRepository catalogRepository)
        {
            _productRepository = productRepository;
            _catalogRepository = catalogRepository;
        }

        #endregion

        #region Public Methods

        public async Task<bool> CreateProductAsync(ProductDto productDto, int userId)
        {
            // Make default catalog name as empty space for id = 0
            var catalogId = default(int);

            if (productDto == null)
                return false;

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

            return await _productRepository.AddProductAsync(product);
        }

        public IEnumerable<ProductEntity> ReadProducts(int userId)
        {
            return _productRepository.GetUserProducts(userId);
        }

        #endregion
    }
}