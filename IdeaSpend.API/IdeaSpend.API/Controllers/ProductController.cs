using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace IdeaSpend.API
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Private Members

        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProductController(ProductService productService,
                                 IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// NOTE: {userId} word inside HttpPost parameter must be the same as userId word inside AddProduct argument
        /// </summary>
        [HttpPost("add/{userId}")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto, int userId)
        {
            if (!await _productService.CreateProductAsync(productDto, userId))
                return BadRequest("Nie udało się zapisać produktu");

            return StatusCode(201);
        }

        [HttpGet("get/{userId}")]
        public IActionResult GetProducts(int userId)
        {
            var userProducts = _productService.ReadProducts(userId);
            var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(userProducts);

            return Ok(productsToReturn);
        }
    }
}
