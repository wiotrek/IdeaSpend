using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        #region Private Members

        private readonly TransactionService _transactionService;

        #endregion

        #region Constructor

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        #endregion

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddTransaction( [FromBody] TransactionDto transactionDto, int userId )
        {
            if( !await _transactionService.CreateTransaction ( transactionDto, userId ) )
                return BadRequest ( "Nie udało się zarejestrować transakcji" );

            return StatusCode ( 201 );
        }
    }
}