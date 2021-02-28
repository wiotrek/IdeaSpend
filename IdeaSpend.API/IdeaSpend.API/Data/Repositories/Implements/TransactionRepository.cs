using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Płatności table which is implemented by <see cref="BaseRepository"/>
    /// </summary>
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        #region Constructor

        public TransactionRepository(IdeaSpendContext dataContext) : base(dataContext) { }

        #endregion

        #region Implemented Methods

        /// <summary>
        /// Saving transaction to db
        /// </summary>
        public async Task<bool> AddTransaction( TransactionEntity transaction )
        {
            await _dataContext.AddAsync ( transaction );
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public IEnumerable<TransactionEntity> GetTransaction(int userId)
        {
            return _dataContext.Transactions
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToList();
        }
        
        #endregion
    }
}