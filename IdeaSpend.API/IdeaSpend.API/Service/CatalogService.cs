using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public class CatalogService
    {
        #region Private Members

        private readonly ICatalogRepository _catalogRepository;
        private readonly IProductRepository _productRepository;

        #endregion

        
        #region Constructor

        public CatalogService(ICatalogRepository catalogRepository,
                              IProductRepository productRepository)
        {
            _catalogRepository = catalogRepository;
            _productRepository = productRepository;
        }

        #endregion


        #region Public Methods

        public async Task<bool> AddCatalog( CatalogDto catalogDto, int userId )
        {
            if( catalogDto == null )
                return false;

            // Only unique names for catalog
            if( _catalogRepository.IsExistCatalog ( catalogDto.CatalogName, userId ) )
                return false;
            
            // new catalog to add with properties from view
            var catalogEntity = new CatalogEntity
            {
                CatalogName = catalogDto.CatalogName,
                UserId = userId
            };

            return await _catalogRepository.CreateCatalogAsync ( catalogEntity );
        }

        public IEnumerable<CatalogEntity> Catalogs(int userId)
        {
            return _catalogRepository.GetCatalogs( userId );
        }

        /// <summary>
        /// All products which assigned to deleting category are moved to default category
        /// </summary>
        public bool DeleteCatalog(int userId, string catalogName)
        {
            // Get result of the delete action
            var result = catalogName != "Domyślny" && 
                         _catalogRepository.DeleteCatalog( userId, catalogName );

            // If something was wrong return false;
            if( !result ) 
                return false;
            
            // Otherwise
            // Get products of the deleted category
            var products = _productRepository.GetUserProductsByCatalogId ( userId, null );
            
            // Get id of the default category
            var defaultCatalogId = _catalogRepository.FindCatalogIdByName ( "Domyślny", userId );

            // For every product without category...
            foreach ( var product in products )
            {
                // assign default category
                product.CatalogId = defaultCatalogId;
                _productRepository.Update ( product );
                _productRepository.SaveAll();
            }

            return true;
        }
        
        #endregion
    }
}