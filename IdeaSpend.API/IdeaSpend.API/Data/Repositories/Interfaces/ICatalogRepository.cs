using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public interface ICatalogRepository 
    {
        Task<bool> CreateCatalogAsync(CatalogEntity catalogEntity);
        int FindCatalogIdByName(string catalogName);
        bool IsExistCatalog( string catalogName, int userId );
        IEnumerable<CatalogEntity> GetCatalogs(int userId);
    }
}