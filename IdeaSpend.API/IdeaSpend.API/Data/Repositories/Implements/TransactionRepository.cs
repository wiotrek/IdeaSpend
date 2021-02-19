using System.Threading.Tasks;

namespace IdeaSpend.API
{
    /// <summary>
    /// CRUD operation for Płatności table
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        #region Private Members

        /// <summary>
        /// The scope application data context
        /// </summary>
        private readonly IdeaSpendContext _dataContext;

        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor

        public TransactionRepository(IdeaSpendContext dataContext, IProductRepository productRepository)
        {
            _dataContext = dataContext;
            _productRepository = productRepository;
        }

        #endregion
        
        public async Task<bool> AddTransaction( TransactionDto transactionDto, int userId )
        {
            // Split ProductNameFrom on product name and seller
            var productName_Seller = transactionDto.ProductNameFrom.Split ( " - " );

            // Get product id for identify product with transaction
            var productId =
                _productRepository.FindProductIdByNameAndSeller ( productName_Seller[0], productName_Seller[1] );
            
            // Create transaction to add
            var transaction = new TransactionEntity
            {
                UserId = userId,
                ProductId = productId.ProductId,
                Quantity = transactionDto.Quantity,
                Weights = transactionDto.Weights,
                Currency = transactionDto.Currency,
                TransactionDate = transactionDto.TransactionDate,
                Paid = transactionDto.Weights * productId.Price
            };

            await _dataContext.AddAsync ( transaction );

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}