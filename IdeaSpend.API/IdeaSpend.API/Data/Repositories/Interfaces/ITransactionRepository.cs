using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Płatności table
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Saving transaction to db
        /// </summary>
        Task<bool> AddTransaction(TransactionEntity transaction);
    }
}