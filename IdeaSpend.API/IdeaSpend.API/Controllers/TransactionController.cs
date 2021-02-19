using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        #region Private Members

        private readonly ITransactionRepository _transactionRepository;

        #endregion

        #region Constructor

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        #endregion

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddTransaction( [FromBody] TransactionDto transactionDto, int userId )
        {
            if( !await _transactionRepository.AddTransaction ( transactionDto, userId ) )
                return BadRequest ( "Nie udało się zarejestrować transakcji" );

            return StatusCode ( 201 );
        }
    }
}