using System.Linq;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Płatności table
    /// </summary>
    public interface ITransactionRepository : IBaseRepository
    {
        /// <summary>
        /// Saving transaction to db
        /// </summary>
        Task<bool> AddTransaction(TransactionEntity transaction);
        IQueryable GetTransaction(int userId);
    }
}