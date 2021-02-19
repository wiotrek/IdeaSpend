using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Private Members

        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        /// <summary>
        /// NOTE: {userId} word inside HttpPost parameter must be the same as userId word inside AddProduct argument
        /// </summary>
        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto, int userId)
        {
            if (!await _productRepository.AddProductAsync(productDto, userId))
                return BadRequest("Nie udało się zapisać produktu");

            return StatusCode(201);
        }
    }
}
