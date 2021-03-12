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

        private async Task CreateProductAsync( ProductDto productDto, int userId )
        {
            // Check differents constraints
            if (!ProductPropertiesValidate(productDto)) return;

            // If user choose any catalog then get id of the catalog
            // otherwise assing product to default category
            var catalogId = _catalogRepository.FindCatalogIdByName ( 
                    productDto.CatalogName != "wybierz kategorie" ? 
                    productDto.CatalogName : "Domyślny" 
                );

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

            await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> GetListThenCreateProductAsync(IEnumerable<ProductDto> allProductsDto, int userId)
        {
            foreach (var productDto in allProductsDto)
            {
                await CreateProductAsync(productDto, userId);
            }
            return true;
        }
        
        public IEnumerable<ProductEntity> ReadProducts(int userId)
        {
            return _productRepository.GetUserProducts(userId);
        }

        /// <summary>
        /// Get products which contain product name or seller
        /// </summary>
        /// <param name="productProperty">product name or seller</param>
        /// <returns>Prodcuts which contain at least one matching sign of the property</returns>
        public IEnumerable<ProductEntity> ReadProductsByNameOrSeller( int userId, string productProperty )
        {
            // Initialazing
            var productsContainingName = _productRepository.FindProductByName ( userId, productProperty ).ToList();
            var productsContainingSeller = _productRepository.FindProductBySeller ( userId, productProperty ).ToList();

            
            // List for collect matching product
            var productsToReturn = new List<ProductEntity>();
            
            
            // Eliminate duplicate products from seller list
            for(var i = 0; i < productsContainingName.Count; i++)
                for(var j = i; j < productsContainingSeller.Count; j++)
                    if (productsContainingName[i].ProductId == productsContainingSeller[j].ProductId)
                        productsContainingSeller.RemoveAt ( j );
            
            
            // If any in with name then copy content
            productsToReturn = productsContainingName.ToList();

            
            // copy missing product with matching seller property
            productsToReturn.AddRange ( productsContainingSeller );


            return productsToReturn.OrderBy(d => d.ProductName);
        }
        
        public bool DeleteProduct(int userId, int productId)
        {
            return _productRepository.DeleteProduct(userId, productId);
        }

        #endregion

        #region Private Methods

        private bool ProductPropertiesValidate( ProductDto productDto )
        {
            if (productDto == null)
                return false;

            // Product name must contain whatever
            if( string.IsNullOrWhiteSpace ( productDto.ProductName ) )
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