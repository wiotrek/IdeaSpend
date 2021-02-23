using System.Threading.Tasks;

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

        #endregion
    }
}