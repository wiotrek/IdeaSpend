using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        #region Private Members

        private readonly TransactionService _transactionService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public TransactionController(TransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        #endregion

        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddTransaction( [FromBody] IEnumerable<TransactionDto> transactionDtos, int userId )
        {
            foreach ( var transactionDto in transactionDtos )
            {
                if( !await _transactionService.CreateTransaction ( transactionDto, userId ) )
                    return BadRequest ( "Nie udało się zarejestrować transakcji" );
            }
            
            return StatusCode ( 201 );
        }

        [HttpGet("get/{userId}")]
        public IActionResult GetTransaction(int userId)
        {
            var result = _transactionService.ReadTransaction(userId);
            var betterResult = _mapper.Map<IEnumerable<TransactionDto>>(result);
            return Ok(betterResult);
        }
        
        [HttpGet("get/last/{userId}")]
        public IActionResult GetLast5Transaction(int userId)
        {
            var result = _transactionService.ReadTransaction(userId, 5);
            var betterResult = _mapper.Map<IEnumerable<TransactionDto>>(result);
            return Ok(betterResult);
        }

        [HttpGet("get/{userId}/{seller}")]
        public IActionResult GetTransactionBySeller( int userId, string seller )
        {

            var transactions = _transactionService.ReadTransactionBySeller ( userId, seller );
            var transactionsToReturn = _mapper.Map<IEnumerable<TransactionDto>> ( transactions );
            
            return Ok(transactionsToReturn);
        }
        
    }
}