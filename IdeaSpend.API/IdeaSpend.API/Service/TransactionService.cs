using System.Linq;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    public class TransactionService
    {
        #region Private Members

        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor

        public TransactionService(ITransactionRepository transactionRepository,
                                                         IProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }

        #endregion

        #region Public Methods

        public async Task<bool> CreateTransaction(TransactionDto transactionDto, int userId)
        {
            // Make sure if any transaction exist to create
            if (transactionDto == null)
                return false;

            // Split ProductNameFrom on product name and seller
            var productName_Seller = transactionDto.ProductNameFrom.Split(" - ");

            // Get product id for identify product with transaction
            var product = _productRepository.FindProductByNameAndSeller(productName_Seller[0], productName_Seller[1]);

            // Creating transaction must be tapped by any product before saving to db
            if (product == null)
                return false;

            // Sum up price single transaction
            double paid = TotalSingleTransactionPaid(product.Price, product.Unit, transactionDto.Weights, transactionDto.Quantity);

            // Create transaction to add
            var transaction = new TransactionEntity
            {
                UserId = userId,
                ProductId = product.ProductId,
                Quantity = transactionDto.Quantity,
                Weights = transactionDto.Weights,
                Currency = transactionDto.Currency,
                TransactionDate = transactionDto.TransactionDate,
                Paid = paid
            };

            return await  _transactionRepository.AddTransaction(transaction);
        }

        /// <summary>
        /// Read specify amount of the transactions
        /// </summary>
        /// <param name="amount">The specify number of the transactions to return</param>
        /// <returns></returns>
        public IQueryable ReadTransaction(int userId, int amount = 0)
        {
            var transactions = amount != 0 ? 
                _transactionRepository.GetTopNTransactions( userId, amount ) : 
                _transactionRepository.GetTransactionByDate(userId);

            return transactions;
        }

        public IQueryable ReadTransactionBySeller( int userId, string seller )
        {
            if( string.IsNullOrWhiteSpace ( seller ) )
                return _transactionRepository.GetTransactionByDate ( userId );
            
            return _transactionRepository.GetTransactionBySeller ( userId, seller );
        }
        
        #endregion

        #region Private Methods

        // TODO: Create enum for unit
        /// <param name="price">The price of the product</param>
        /// <param name="unit">The unit of the product</param>
        /// <param name="weight">Total weight of the bought specific product</param>
        /// <param name="quantity">Total quantity of the bought specific product</param>
        private double TotalSingleTransactionPaid(double price, string unit, double weight = 0, int quantity = 0)
        {
            double totalPrice;
            
            if (unit != "szt")
                totalPrice = price * weight;
            else
                totalPrice = price * quantity;

            return totalPrice;
        }

        #endregion
    }
}
