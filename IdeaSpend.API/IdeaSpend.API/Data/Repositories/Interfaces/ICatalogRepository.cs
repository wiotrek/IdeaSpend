using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public interface ICatalogRepository 
    {
        /// <summary>
        /// Create new catalog
        /// </summary>
        /// <param name="catalogDto">The catalog dto arrived from view form </param>
        /// <param name="userId">User id of the current login user provided from decoded token</param>
        /// <returns></returns>
        Task<bool> AddCatalogAsync(CatalogDto catalogDto, int userId);

        int FindCatalogIdByName(string catalogName);
    }
}