using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public interface ICatalogRepository : IBaseRepository
    {
        Task<bool> CreateCatalogAsync(CatalogEntity catalogEntity);
        int FindCatalogIdByName( string catalogName, int userId = default );
        bool IsExistCatalog( string catalogName, int userId );
        IEnumerable<CatalogEntity> GetCatalogs(int userId);
        bool DeleteCatalog(int userId, string catalogName);
    }
}