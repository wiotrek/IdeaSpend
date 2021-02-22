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
        
        public async Task<bool> CreateCatalogAsync(CatalogEntity catalogEntity)
        {
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
