using System.Collections.Generic;
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
        IEnumerable<TransactionEntity> GetTransaction(int userId);
    }
}