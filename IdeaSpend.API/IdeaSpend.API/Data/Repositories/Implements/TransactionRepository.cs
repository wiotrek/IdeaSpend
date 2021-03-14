using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    // TODO: Move all filter methods to generic package of class and interfaces
    //       responsible for filter items depend on entity 
    
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

        
        public IQueryable GetTransactionByDate(int userId, DateTime date = default)
        {
            if (date == default)
                date = GetDateOfLastTransaction ( userId );
            
            var sqlQuery = 
                from transaction in _dataContext.Set<TransactionEntity>()
                    .Where(i => i.UserId == userId)
                    .Where(d => d.TransactionDate.Year == date.Year)
                    .Where(d => d.TransactionDate.Month == date.Month)
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

        private DateTime GetDateOfLastTransaction(int userId)
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