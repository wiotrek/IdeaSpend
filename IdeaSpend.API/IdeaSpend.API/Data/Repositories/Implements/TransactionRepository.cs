using System.Linq;
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

        public IQueryable GetTransaction(int userId)
        {
            var sqlQuery = 
                from transaction in _dataContext.Set<TransactionEntity>()
                    .Where(i => i.UserId == userId)
                    .OrderByDescending ( d => d.TransactionDate )
                join product in _dataContext.Set<ProductEntity>()
                    on transaction.ProductId equals product.ProductId into grouping
                from product in grouping.DefaultIfEmpty()
                select new
                {
                    productNameFrom = transaction.ProductId.HasValue == false ? "Usunięty" : product.ProductName + " - " + product.Seller,
                    transaction.Currency,
                    transaction.Quantity,
                    transaction.Weights,
                    transaction.TransactionDate,
                    transaction.Paid
                };

            return sqlQuery;

        }

        public IQueryable GetTopNTransactions( int userId, int amount )
        {
            var sqlQuery = 
                from transaction in _dataContext.Set<TransactionEntity>()
                    .Where(i => i.UserId == userId)
                    .OrderByDescending ( d => d.TransactionDate )
                    .Take(amount)
                join product in _dataContext.Set<ProductEntity>()
                    on transaction.ProductId equals product.ProductId into grouping
                from product in grouping.DefaultIfEmpty()
                select new
                {
                    productNameFrom = transaction.ProductId.HasValue == false ? "Usunięty" : product.ProductName + " - " + product.Seller,
                    transaction.Currency,
                    transaction.Quantity,
                    transaction.Weights,
                    transaction.TransactionDate,
                    transaction.Paid
                };

            return sqlQuery;
        }

        #endregion
    }
}