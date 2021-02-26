using System.Collections.Generic;
using System.Linq;
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

            // Check differents constraints
            if (!ProductPropertiesValidate(productDto))
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

        #region Private Methods

        private bool ProductPropertiesValidate( ProductDto productDto )
        {
            if (productDto == null)
                return false;

            // Not allow to put digit anywhere in product name and must contain whatever
            if( string.IsNullOrWhiteSpace ( productDto.ProductName ) ||
                productDto.ProductName.Any ( char.IsDigit ) )
                return false;

            // Only unique products for each seller can be saved
            if( _productRepository.FindProductByNameAndSeller ( productDto.ProductName, productDto.Seller ) != null )
                return false;
            
            // Return true if seller don't have digits or whitespace
            return !string.IsNullOrWhiteSpace ( productDto.Seller ) && 
                   !productDto.Seller.Any ( char.IsDigit );
        }

        #endregion
    }
}