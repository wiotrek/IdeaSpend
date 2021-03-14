using System;
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

        IQueryable GetTransactionByDate( int userId, DateTime date = default );
        
        /// <summary>
        /// Read specify amount of the transactions
        /// </summary>
        /// <param name="amount">The specify number of the transactions to return</param>
        /// <returns></returns>
        IQueryable GetTopNTransactions( int userId, int amount );

        IQueryable GetTransactionBySeller( int userId, string seller );
    }
}