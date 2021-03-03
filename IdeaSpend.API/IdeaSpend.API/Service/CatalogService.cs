using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public class CatalogService
    {
        #region Private Members

        private readonly ICatalogRepository _catalogRepository;

        #endregion

        
        #region Constructor

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
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
            return _catalogRepository.GetCatalogs(userId);
        }

        public async Task<bool> DeleteCatalog(int catalogId)
        {
            return _catalogRepository.DeleteCatalog(catalogId);
        }
        
        #endregion
    }
}