using System.Linq;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public class CatalogRepository : ICatalogRepository
    {
        #region Private Members

        /// <summary>
        /// The scope application data context
        /// </summary>
        private readonly IdeaSpendContext _dataContext;

        #endregion

        #region Constructor

        public CatalogRepository(IdeaSpendContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implemented Methods

        /// <summary>
        /// Create new catalog
        /// </summary>
        /// <param name="catalogDto">Object with base information of the catalog entity</param>
        /// <param name="userId">User id of the current login user provided from decoded token</param>
        /// <returns></returns>
        public async Task<bool> AddCatalogAsync(CatalogDto catalogDto, int userId)
        {
            // new catalog to add with properties from view
            var catalogEntity = new CatalogEntity
            {
                CatalogName = catalogDto.CatalogName,
                UserId = userId
            };

            await _dataContext.AddAsync(catalogEntity);

            return await _dataContext.SaveChangesAsync() > 0;
        }

        public int FindCatalogIdByName(string catalogName)
        {
            // Go to catalog table
            var id = _dataContext.Catalogs

                // Get first match catalog id filtered by catalog name
                .FirstOrDefault( n => n.CatalogName == catalogName ).CatalogId;

            return id;
        }

        #endregion
    }
}
