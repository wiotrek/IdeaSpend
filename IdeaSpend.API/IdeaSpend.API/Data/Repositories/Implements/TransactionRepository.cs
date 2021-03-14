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

        /// <summary>
        /// Finding transactions made with indicated date
        /// </summary>
        /// <param name="date">The date which transactions made</param>
        public IQueryable GetTransactionByDate(int userId, string date = default)
        {
            if (date == default)
                date = GetDateOfLastTransaction ( userId );
            
            var sqlQuery = 
                from transaction in _dataContext.Set<TransactionEntity>()
                    .Where(i => i.UserId == userId)
                    .Where(d => d.TransactionDate.Substring(0, 4) == date.Substring(0, 4))
                    .Where(d => d.TransactionDate.Substring(5, 2) == date.Substring(5, 2))
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

        /// <summary>
        /// Finding first N={1, 2, 3, ...} transactions
        /// </summary>
        /// <param name="amount">The amount transactions to display</param>
        /// <returns>transactions from latest to first</returns>
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

        /// <summary>
        /// Finding first and last transaction
        /// </summary>
        public string[] GetRangeDate( int userId )
        {
            var first = GetDateOfFirstTransaction ( userId );
            var last = GetDateOfLastTransaction ( userId );

            return new[] {first, last};
        }

        /// <summary>
        /// Searching transactions which contains at least one sign of the seller
        /// </summary>
        /// <param name="seller">The seller to matching by contain signs</param>
        /// <returns>All transactions with common sign if founded, empty if not founded
        ///          all transactions if seller is null or white space</returns>
        public IQueryable GetTransactionBySeller( int userId, string seller )
        {
            var sqlQuery = 
                
                // get transactions
                from transaction in _dataContext.Set<TransactionEntity>()
                .Where(i => i.UserId == userId)
                .Where(s => s.Product.Seller.Contains(seller))
                
                // join products
                join product in _dataContext.Set<ProductEntity>()
                    on transaction.ProductId equals product.ProductId into grouping
                
                // attach products
                from product in grouping.DefaultIfEmpty()

                // get information of the result
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
        
        #region Private Methods
        
        /// <summary>
        /// Finding first transaction user made
        /// </summary>
        private string GetDateOfFirstTransaction(int userId)
        {
            var date = _dataContext.Transactions
                .Where ( u => u.UserId == userId )
                .OrderBy ( d => d.TransactionDate )
                .FirstOrDefault().TransactionDate;
            
            return date;
        }

        /// <summary>
        /// Finding last transaction user made
        /// </summary>
        private string GetDateOfLastTransaction(int userId)
        {
            var date = _dataContext.Transactions
                .Where ( u => u.UserId == userId )
                .OrderByDescending ( d => d.TransactionDate )
                .FirstOrDefault().TransactionDate;
            
            return date;
        }
        
        #endregion
    }
}