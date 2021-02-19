using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Płatności table
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Create single transaction
        /// </summary>
        /// <returns></returns>
        Task<bool> AddTransaction(TransactionDto transactionDto, int userId);
    }
}