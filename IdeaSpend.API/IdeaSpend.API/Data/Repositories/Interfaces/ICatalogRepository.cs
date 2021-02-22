using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public interface ICatalogRepository 
    {
        Task<bool> CreateCatalogAsync(CatalogEntity catalogEntity);
        int FindCatalogIdByName(string catalogName);
    }
}